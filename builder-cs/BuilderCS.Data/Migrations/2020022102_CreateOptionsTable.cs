

using FluentMigrator;

namespace BuildersFS.Data.Migrations
{
  [Migration(2020022102)]
  public class CreateOptionsTable : Migration
  {
    public override void Down()
    {
      Delete.Table("options");
    }

    public override void Up()
    {
      Create.Table("options")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("name").AsString(50).NotNullable().Unique()
        .WithColumn("option_value").AsString().Nullable()
        ;
    }
  }
}