
using System;
using FluentMigrator;

namespace BuildersFS.Data.Migrations
{
  [Migration(2020022105)]
  public class CreateRequisitionsTable : Migration
  {
    public override void Up()
    {
      AddCostTypesTable();
      AddRequisitionsTable();
      AddRequisitionItemsTable();
    }
    public override void Down()
    {
      Delete.Table("requisition_items");
      Delete.Table("requisitions");
      Delete.Table("cost_types");
    }
    private void AddRequisitionItemsTable()
    {
      Create.Table("requisition_items")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("requisition_id").AsGuid().NotNullable().Unique()
        .WithColumn("sno").AsByte().NotNullable()
        .WithColumn("item_desc").AsAnsiString().NotNullable()
        .WithColumn("uom").AsAnsiString(50).NotNullable()
        .WithColumn("qty").AsDecimal(14, 2).NotNullable()
        .WithColumn("est_price").AsDecimal(14, 2).NotNullable();

      Create.Index()
        .OnTable("requisition_items")
        .OnColumn("requisition_id").Ascending()
        .OnColumn("sno").Ascending();

      Create.ForeignKey()
        .FromTable("requisition_items").ForeignColumn("requisition_id")
        .ToTable("requisitions").PrimaryColumn("id");

    }

    private void AddRequisitionsTable()
    {
      Create.Table("requisitions")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("trans_date").AsDate().NotNullable()
        .WithColumn("ref_no").AsAnsiString(100).NotNullable().Unique()
        .WithColumn("dept_id").AsGuid().NotNullable().Indexed()
        .WithColumn("sub_dept_id").AsGuid().Nullable().Indexed()
        .WithColumn("project_id").AsGuid().Nullable().Indexed()
        .WithColumn("boq_item_id").AsGuid().Nullable().Indexed()
        .WithColumn("cost_type_id").AsByte().NotNullable()
        .WithColumn("requested_by").AsAnsiString(100).Nullable()
        .WithColumn("date_required").AsDate().NotNullable()
        .WithColumn("approved_by").AsAnsiString(100).Nullable()
        .WithColumn("date_approved").AsDate().Nullable()
        .WithColumn("est_total").AsDecimal(14, 2).NotNullable()
        .WithColumn("status_id").AsInt16().WithDefaultValue(0).Indexed()
        .WithColumn("cancelled_by").AsAnsiString(100).Nullable()
        .WithColumn("cancel_reason").AsAnsiString(200).Nullable();

      Create.Index().OnTable("requisitions")
        .OnColumn("trans_date").Ascending()
        .OnColumn("project_id").Ascending()
        .OnColumn("dept_id").Ascending()
        .OnColumn("boq_item_id").Ascending();

      Create.ForeignKey()
        .FromTable("requisitions").ForeignColumn("cost_type_id")
        .ToTable("cost_types").PrimaryColumn("id");

      Create.ForeignKey()
        .FromTable("requisitions").ForeignColumn("project_id")
        .ToTable("projects").PrimaryColumn("id");

      Create.ForeignKey()
        .FromTable("requisitions").ForeignColumn("boq_item_id")
        .ToTable("boq_items").PrimaryColumn("id");
    }

    private void AddCostTypesTable()
    {
      Create.Table("cost_types")
        .WithColumn("id").AsByte().NotNullable().PrimaryKey()
        .WithColumn("name").AsAnsiString(50).NotNullable().Indexed();

      Insert.IntoTable("cost_types").Row(new { id = 1, name = "Material" });
      Insert.IntoTable("cost_types").Row(new { id = 2, name = "Equipment" });
      Insert.IntoTable("cost_types").Row(new { id = 3, name = "Labour" });
      Insert.IntoTable("cost_types").Row(new { id = 4, name = "Subcontractor" });
    }


  }
}