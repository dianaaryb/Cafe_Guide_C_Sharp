Install tooling

~~~bash
dotnet tool update -g dotnet-ef
dotnet tool update -g dotnet-aspnet-codegenerator
~~~

~~~bash
Run from solution folder
dotnet ef migrations --project App.DAL.EF --startup-project WebApp add StrangeIntDeleted
dotnet ef database   --project App.DAL.EF --startup-project WebApp update
dotnet ef database   --project App.DAL.EF --startup-project WebApp drop
~~~

MVC controllers

Run from WebApp folder!

~~~bash
cd WebApp

dotnet aspnet-codegenerator controller -name CafesController        -actions -m  App.Domain.Cafe        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafeCategoriesController        -actions -m  App.Domain.CafeCategory        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafeOccasionsController        -actions -m  App.Domain.CafeOccasion        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafePhotosController        -actions -m  App.Domain.CafePhoto        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafeTypesController        -actions -m  App.Domain.CafeType        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryOfCafesController        -actions -m  App.Domain.CategoryOfCafe        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CitiesController        -actions -m  App.Domain.City        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FavouritesController        -actions -m  App.Domain.Favourite        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenusController        -actions -m  App.Domain.Menu        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuItemsController        -actions -m  App.Domain.MenuItem        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuItemCategoriesController        -actions -m  App.Domain.MenuItemCategory        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuItemPhotosController        -actions -m  App.Domain.MenuItemPhoto        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OccasionOfCafesController        -actions -m  App.Domain.OccasionOfCafe        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReviewsController        -actions -m  App.Domain.Review        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReviewPhotosController        -actions -m  App.Domain.ReviewPhoto        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TypeOfCafesController        -actions -m  App.Domain.TypeOfCafe        -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

#use area
dotnet aspnet-codegenerator controller -name CafesController        -actions -m  App.Domain.Cafe        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafeCategoriesController        -actions -m  App.Domain.CafeCategory        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafeOccasionsController        -actions -m  App.Domain.CafeOccasion        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafePhotosController        -actions -m  App.Domain.CafePhoto        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CafeTypesController        -actions -m  App.Domain.CafeType        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CategoryOfCafesController        -actions -m  App.Domain.CategoryOfCafe        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CitiesController        -actions -m  App.Domain.City        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name FavouritesController        -actions -m  App.Domain.Favourite        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenusController        -actions -m  App.Domain.Menu        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuItemsController        -actions -m  App.Domain.MenuItem        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuItemCategoriesController        -actions -m  App.Domain.MenuItemCategory        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MenuItemPhotosController        -actions -m  App.Domain.MenuItemPhoto        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name OccasionOfCafesController        -actions -m  App.Domain.OccasionOfCafe        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReviewsController        -actions -m  App.Domain.Review        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ReviewPhotosController        -actions -m  App.Domain.ReviewPhoto        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TypeOfCafesController        -actions -m  App.Domain.TypeOfCafe        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


cd ..
~~~

Api controllers
~~~bash
dotnet aspnet-codegenerator controller -name CafesController -m  App.Domain.Cafe        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CafeCategoriesController -m  App.Domain.CafeCategory        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CafeOccasionsController -m  App.Domain.CafeOccasion        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CafePhotosController -m  App.Domain.CafePhoto        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CafeTypesController -m  App.Domain.CafeType        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CategoryOfCafesController -m  App.Domain.CategoryOfCafe        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name CitiesController -m  App.Domain.City        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name FavouritesController -m  App.Domain.Favourite        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MenusController -m  App.Domain.Menu        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MenuItemsController -m  App.Domain.MenuItem        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MenuItemCategoriesController -m  App.Domain.MenuItemCategory        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MenuItemPhotosController -m  App.Domain.MenuItemPhoto        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name OccasionOfCafesController -m  App.Domain.OccasionOfCafe        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ReviewsController -m  App.Domain.Review        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ReviewPhotosController -m  App.Domain.ReviewPhoto        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TypeOfCafesController -m  App.Domain.TypeOfCafe        -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f



~~~


//Host cant be null
dotnet ef database update --connection "Host=localhost;Port=7890;Database=cafeguide;Username=postgres;Password=postgres" --project App.DAL.EF --startup-project WebApp
dotnet ef database drop --connection "Host=localhost;Port=7890;Database=cafeguide;Username=postgres;Password=postgres" --project App.DAL.EF --startup-project WebApp




TESTING
in Base solution, we test
-Base.DAL.EF: BaseEntityRepository in BaseRepositoryTest


//Docker
~~~bash
#docker buildx build --progress=plain --force-rm -t diryb/webapp:latest --push .
#docker buildx build --progress=plain --force-rm --push -t akaver/webapp:latest .
docker build -t webapp:latest .

# multiplatform build on apple silicon
# https://docs.docker.com/build/building/multi-platform/
#docker buildx create --name mybuilder --bootstrap --use
docker buildx build --platform linux/amd64 -t webapp:latest .
docker buildx build --platform linux/amd64 -t diryb/webapp:latest --push .

~~~