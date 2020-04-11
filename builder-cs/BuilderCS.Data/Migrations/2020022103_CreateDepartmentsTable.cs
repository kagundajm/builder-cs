

using System;
using FluentMigrator;

namespace BuildersFS.Data.Migrations
{

  [Migration(2020022103)]
  public class CreateDepartmentsTable : Migration
  {
    public override void Down()
    {
      Delete.Table("departments");
    }

    public override void Up()
    {
      CreateTable();
      InsertDefaultData();
    }

    private void CreateTable()
    {
      Create.Table("departments")
        .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
        .WithColumn("name").AsString(100).NotNullable().Unique()
        .WithColumn("parent_id").AsGuid().Nullable().Indexed()
        .WithColumn("code").AsInt32().NotNullable().Unique()
        .WithColumn("depth").AsByte().NotNullable()
        .WithColumn("is_parent").AsBoolean().WithDefaultValue(true);

      Create.ForeignKey()
        .FromTable("departments").ForeignColumn("parent_id")
        .ToTable("departments").PrimaryColumn("id");
    }

    private void InsertDefaultData()
    {

      var qry = @"TRUNCATE departments;
          INSERT INTO departments (id, name, parent_id, code, depth, is_parent) VALUES
            ('2e7969b9-3313-4a97-9d79-5017d0223a19', 'Finance',	NULL,	100000,	1,	'1'),
            ('85108b6a-e3dd-43f1-818d-253b7240f95c',	'Accounting',	'2e7969b9-3313-4a97-9d79-5017d0223a19',	100100,	1,	'1'),
            ('3169bee2-2a58-407b-b83f-434ce4c086bc',	'Engineering',	NULL,	110000,	1,	'1'),
            ('846d761e-a02d-447b-af94-45d26c1e57e1',	'Human Resource',	NULL,	130000,	1,	'1');";

      Execute.Sql(qry);
    }
  }
}