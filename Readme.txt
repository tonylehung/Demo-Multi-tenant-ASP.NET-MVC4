Multit-tenancy using SimpleMembership
-------------------
Requirement:

- Visual Studio 2012
- .Net framework 4, MVC 4
- MS SQL Server 2008 R2

-------------------
Step by step:

Step 1:
	- Attach database with 'ort.mdf' or restore with 'ort.bak' to MS SQL Server, we also create database by query 'ort.sql'
	- Create an account and set all roles for it: username: ort, password: ort@1234
Step 2:
	- We have to reset connection string to server stored the data on web folder at 'TenantID/Web.config' and 'TenantURL/Web.config'
	- Open project with Visual Studio 2012 or add the website to IIS to debug it

Step 3:
	- Test with default accounts on database that's we retored:
		usernam: hunglx, pw: hunglx or username: admin, pw: adminadmin
	
	- If we created database by query then on the website > we have to register an account.
	- Login to website and register a tenancy at 'Tenant control', for example:
		Tenant user: as username
		Theme: 'cnn' or 'bbc'
		Tenant schema: 'cnn' or 'bbc'
		Tenant Connection: Data Source=cn1.data.vology.com,1433;Initial Catalog=ort;persist security info=True;user id=ort;password=ort@1234;MultipleActiveResultSets=True;App=EntityFramework
	- Click to "Keep your news" to create an record on database seperated distinguish schema 'cnn.News' and 'bbc.News'. Each account is set up in 'Tenan control' will correspond generally used on a table News


-------------------
Reference appendix:

Tenant ID
localhost/default/register ~ login
localhost/controller/action/id ~ different UI

Tenant URL Routing
localhost/default/register	~ different UI
localhost/tenant/controller/action/id ~ different UI
localhost/tenant/controller/action/id ~ different UI


There are a number of options for identifying the tenant from a web request.

Tenant ID
- Authentication. If users accessing the site must authenticate, the site can determine the tenant from the authenticated identity.

URL ROUTING
- The URL path. For example, Web’s tenants A and B could use:
        http://localhost/A/id
        http://localhost/B/id

- The subdomain. For example, Web’s tenants A and B could use 
         http://a.localhost/id 
         http://b.localhost/id

- A custom domain. For example, Web’s tenants A and B could use 
         http://localhost.b.com
         http://localhost.a.com

http://msdn.microsoft.com/en-us/library/hh534477.aspx
http://www.sapiensworks.com/blog/post/2012/03/01/AspNet-Mvc-Routing-With-Multi-Tenant-Support.aspx
http://www.developer.com/design/article.php/3801931/Introduction-to-Multi-Tenant-Architecture.htm
http://msdn.microsoft.com/en-us/library/aa479086.aspx
http://www.dennisonpro.info/simple-multitenancy-with-asp-net-mvc-4/
http://www.codeproject.com/Articles/51334/Multi-Tenants-Database-Architecture