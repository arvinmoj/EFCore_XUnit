Install Nuget Package (DataAccessLayer)

 <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="3.1.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="3.1.9">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="3.1.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="3.1.9">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="3.1.9" />
  </ItemGroup>

Install Nuget Package (DataAccessLayer.Test)

 <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="3.1.9" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.9" />
    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.console" Version="2.4.1">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

Scaffolding Entity
Models Making with Scaffolding (Models in sqlserver Make Models in MyProject)

    dotnet ef dbcontext scaffold 
    ex: dotnet ef dbcontext scaffold "Data Source = Windows\SQLSERVER;Initial Catalog=AdventureWorks2016;Integrated Security=True" Microsoft.EntityframeworkCore.SqlServer -d -c AdventureWorksContext -o EFCore\Enteritis --force