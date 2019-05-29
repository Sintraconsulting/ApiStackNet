## SET UP

## 1) NEW PROJECT
create a new web api project called 'ApiStackNet.Demo'.

## 2) DAL

Inherith Base entity and configure EF mapping 

```cs

    [Table("MYTABLE", Schema = "DBO")]
    public class MyTable : AuditableEntity<Int32>
    {        
        public string Field { get; set; }
    }

```

using auditable entity audit fields are included by design. To adapt field mapping just override fields.



```cs

    [Table("MYTABLE", Schema = "DBO")]
    public class MyTable : AuditableEntity<Int32>
    {   
		[Column("MY_CUSTOM_NAME")]
        public string Field { get; set; }

		public override DateTime CreatedOn { get; set; }
    }

```


## 3) BLL

There are two different class to implement:
- BO (Business Object) is the class used to save data. This contains only relevant fields for saving
- DTO (Data Transfert Object) is the class used to profide data, after business logic manipulation.


```cs
 public class MyTableDTO:AuditableEntity<Int32>
{
	public string EntityName { get; set; }
	//other computed or read-only fields here
}


    public class MyTableBO:BaseEntity<Int32>
    {
        public string EntityName { get; set; }
		//no computed fields here
		//no read only fields
		
    }

```


## 4) Mapping
We need two mapping class (entity -> DTO) (BO ->entity), used to fill DTO and save data.

```cs
public class MyTablegBO2EMapping:Profile
{
    public MyTablegBO2EMapping()
    {
        CreateMap<MyTableBO, MyTable>(); //NO reverse map.ReverseMap();
    }
}


public class MyTableE2DTOMapping : Profile
{
    public MyTableE2DTOMapping()
    {
        CreateMap<MyTable, MyTableDTO>();// No Reverse map ! .ReverseMap();
    }
}
```
Each mapping is implemented by a AutoMapper profile. This part can be also managed by using only one profile, but when classes grow may be harder to manage.



## 5) BLL
Business logic layer is impemented using Service Pattern. To get all basic CRUD functionality just implement a class:

```cs
 public class  MyTableDataService: IntDataService<MyTableDTO, MyTableBO,MyTable>
    {
        public  MyTableDataService(DbContext context, IMapper mapper) : base( context,  mapper)
        {
        
        }
    }
```
This configuration tell the relationship between entity, BO, DTO. 

**What class to inherit?**
There is one service for primary key type, naming convention is <PK>DataService, so you can have `IntDataService`, `GuidDataService` and so on.



## 6) Controller

Implementing a Controller extending `DataController`, this bring to you all CRUD functionalities.


```cs
    public class MyTableController : DataController<MyTableDataService, MyTableDTO, MyTableBO, MyTable, int>
    {
        public MyTableController(MyTableDataService service) : base(service)
        {
        }
    }
```

 Each field can be searched from client. This work only for entity field. Related search (i.e. get all project of a given user) must be implemented by custom  method or method extension.


```json
{
  "PageNumber": 1,
  "PageSize": 10,
  "Filter": [
    {
      "Name": "FieldName",
      "Comparator": "Equal", 
      "Value": "true"
    }
  ]
}

```

## 7) AUTOMAPPER AND DBCONTEXT
download Automapper nuget package and create a class inside the new project called 'ApiStackNetDBContext', this is the primary class 
that is responsible for interacting with the database.

``` cs
public class ApiStackNetDBContext:ApiStackDbContext
{
    public ApiStackNetDBContext() : base("ApiStackNetDBContext")
    {
        Database.SetInitializer<ApiStackNetDBContext>(null);
        this.Configuration.LazyLoadingEnabled = false;

        EnableVerboseSQLog();
    }

    //entities
    public DbSet<MyTable> MyTable { get; set; }
}
```

## 8) EXECUTE COMMANDS
you need to execute the following commands in the Package Manager Console in Visual Studio in this order:

-Enable-Migrations: Enables the migration in the project by creating a Configuration class.
-Add-Migration: Creates a new migration class as per specified name with the Up() and Down() methods.
-Update-Database: Executes the last migration file created by the Add-Migration command and applies changes to the database schema.


## 9) AUTOFAC
download Autofac nuget package and add a new class called 'AutofacConfig' and add this

``` cs
using System.Web;
using System.Web.Compilation;
using System.Web.Http;

namespace ApiStackNet.Demo.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterContainer(HttpConfiguration config)
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            
            builder.RegisterType<ApiStackNetDBContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<MyTableDataService>().AsSelf().InstancePerRequest();
            builder.RegisterType<MessageService>().AsSelf().InstancePerRequest();
            builder.RegisterModule(new MapperInstaller());

            var container = builder.Build();

            Mapper.Initialize(x => x.ConstructServicesUsing(container.Resolve));

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
```

and finally add this line at the beginning of the Application_Start() method of the Global.asax.cs class.

``` cs
AutofacConfig.RegisterContainer(GlobalConfiguration.Configuration);
```
