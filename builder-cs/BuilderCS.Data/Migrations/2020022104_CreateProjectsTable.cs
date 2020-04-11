using System;
using FluentMigrator;

namespace BuildersFS.Data.Migrations
{
  // Variations
  // Running Bill
  // Measurement Book - https://www.youtube.com/watch?v=cGCjukFtxVA

  [Migration(2020022104)]
  public class CreateProjectTable : Migration
  {
    public override void Down()
    {
      Delete.Table("boq_items");
      Delete.Table("boq_sections");
      Delete.Table("projects");
    }
    public override void Up()
    {
      CreateProjectsTable();
      CreateBoqSectionsTable();
      CreateBoqItemsTable();
    }

    private void CreateBoqItemsTable()
    {
      Create.Table("boq_items")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("project_id").AsGuid().NotNullable().Indexed()
        .WithColumn("item_no").AsAnsiString(50).NotNullable().Unique()
        .WithColumn("activity_name").AsAnsiString().NotNullable()
        .WithColumn("pg_no").AsInt16().NotNullable()
        .WithColumn("units").AsAnsiString(50).NotNullable()
        .WithColumn("qty").AsDecimal(14, 2).NotNullable()
        .WithColumn("rate").AsDecimal(14, 2).NotNullable()
        .WithColumn("bq_amt").AsDecimal(14, 2).NotNullable()
        .WithColumn("budget").AsDecimal(14, 2).NotNullable().WithDefaultValue(0)
        .WithColumn("section_id").AsGuid().NotNullable()
        .WithColumn("sub_section_id").AsGuid().Nullable();

      Create.Index()
        .OnTable("boq_items")
        .OnColumn("project_id").Ascending()
        .OnColumn("item_no").Ascending();

      Create.ForeignKey()
        .FromTable("boq_items").ForeignColumn("section_id")
        .ToTable("boq_sections").PrimaryColumn("id");

      Create.ForeignKey()
        .FromTable("boq_items").ForeignColumn("sub_section_id")
        .ToTable("boq_sections").PrimaryColumn("id");

      Create.ForeignKey()
        .FromTable("boq_items").ForeignColumn("project_id")
        .ToTable("projects").PrimaryColumn("id");
    }

    private void CreateBoqSectionsTable()
    {
      Create.Table("boq_sections")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("project_id").AsGuid().NotNullable().Indexed()
        .WithColumn("name").AsAnsiString().NotNullable().Unique()
        .WithColumn("parent_id").AsGuid().Nullable().Indexed();

      Create.ForeignKey()
        .FromTable("boq_sections").ForeignColumn("parent_id")
        .ToTable("boq_sections").PrimaryColumn("id");

      Create.ForeignKey()
        .FromTable("boq_sections").ForeignColumn("project_id")
        .ToTable("projects").PrimaryColumn("id");
    }

    private void CreateProjectsTable()
    {
      Create.Table("projects")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("dept_id").AsGuid().NotNullable().Indexed()
        .WithColumn("sub_dept_id").AsGuid().Nullable().Indexed()
        .WithColumn("contract_no").AsString(150).NotNullable().Unique()
        .WithColumn("name").AsString().NotNullable().Unique()
        .WithColumn("client_name").AsString().NotNullable()
        .WithColumn("coordinator").AsString().NotNullable()
        .WithColumn("url").AsString(200).Nullable()
        .WithColumn("start_date").AsDate().NotNullable()
        .WithColumn("end_date").AsDate().NotNullable()
        .WithColumn("date_completed").AsDate().Nullable()
        .WithColumn("duration").AsString(100).Nullable()
        .WithColumn("contract_amount").AsDecimal(14, 2)
        .WithColumn("status").AsByte().WithDefaultValue(0);

      Create.ForeignKey()
        .FromTable("projects").ForeignColumn("dept_id")
        .ToTable("departments").PrimaryColumn("id");

      Create.ForeignKey()
        .FromTable("projects").ForeignColumn("sub_dept_id")
        .ToTable("departments").PrimaryColumn("id");
    }


  }
}