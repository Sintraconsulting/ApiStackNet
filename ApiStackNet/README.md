
## DAL

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
        public string Field { get; set; }

		[Column("MY_CUSTOM_NAME)]
		public override DateTime CreatedOn { get; set; }
    }

```


## BLL

There are two different class to implement:
- BO (Business Object) is the class used to save data. This contains only relevant fields for saving
- DTO (Data Transfert Object) is the class used to profide data, after business logic manipulation.


```cs
 public class MyTableDTO:AuditableEntity<Int32>
{
	public string EntityName { get; set; }
	//other computed or read-only fields here
}


    public class SyncLogBO:BaseEntity<Int32>
    {
        public string EntityName { get; set; }
		//no computed fields here
		//no read only fields
		
    }

```


## Mapping
We need two mapping class (entity -> DTO) (BO ->entity), used to fill DTO and save data.

```cs
public class SyncLogBO2EMapping:Profile
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



## BLL
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



## Controller

