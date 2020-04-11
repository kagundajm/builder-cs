
using System;
using FluentMigrator;

namespace BuildersFS.Data.Migrations
{
  [Migration(2020022101)]
  public class CreateBudgetAccountsTable : Migration
  {
    public override void Down()
    {
      Delete.Table("accounts");
    }

    public override void Up()
    {
      AddAccountsTable();
      CreateDefaultAccounts();
    }

    private void AddAccountsTable()
    {
      Create.Table("accounts")
        .WithColumn("id").AsGuid().PrimaryKey()
        .WithColumn("name").AsAnsiString(100).NotNullable()
        .WithColumn("account_group").AsAnsiString(20).Nullable().Indexed()
        .WithColumn("account_type").AsAnsiString(80).NotNullable().Indexed()
        .WithColumn("parent_id").AsGuid().Nullable().Indexed()
        .WithColumn("code").AsInt32().NotNullable().Unique()
        .WithColumn("depth").AsByte().NotNullable()
        .WithColumn("is_hidden").AsBoolean().NotNullable().WithDefaultValue(false)
        .WithColumn("is_parent").AsBoolean().WithDefaultValue(false);

      Create.Index().OnTable("accounts")
        .OnColumn("account_group").Ascending()
        .OnColumn("code").Ascending();

      Create.ForeignKey()
        .FromTable("accounts").ForeignColumn("parent_id")
        .ToTable("accounts").PrimaryColumn("id");
    }

    private void CreateDefaultAccounts()
    {
      var qry = @"TRUNCATE accounts;
          INSERT INTO accounts (id, name, account_group, account_type, parent_id, code, depth, is_parent, is_hidden) VALUES
          ('bb317393-d163-4cac-95f1-3ec204e483fe',	'Assets',	'Assets',	'ASSET',	NULL,	1000000,	1,	'1',	'0'),
          ('30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	'Current Assets',	'Assets',	'ASSET',	'bb317393-d163-4cac-95f1-3ec204e483fe',	1010000,	2,	'1',	'0'),
          ('beeb93fc-5b1c-4220-a81c-7b059125b56f',	'Petty Cash',	'Assets',	'CASH',	'30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	1010100,	3,	'1',	'0'),
          ('a9d0c259-6b0c-43e3-8338-4997ebb0e1bf',	'Cashier 01',	'Assets',	'CASH',	'beeb93fc-5b1c-4220-a81c-7b059125b56f',	1010101,	4,	'0',	'0'),
          ('ec01f6e3-e0bd-4b18-9afc-dd50677ba0f2',	'Cashier 02',	'Assets',	'CASH',	'beeb93fc-5b1c-4220-a81c-7b059125b56f',	1010102,	4,	'0',	'0'),
          ('0ee62740-7c92-49b1-b63d-99ead2c3570d',	'Bank Deposits',	'Assets',	'BANK',	'30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	1010200,	3,	'1',	'0'),
          ('28c729b2-b23a-4edb-9753-cf23c2830d3d',	'Bank 01',	'Assets',	'BANK',	'0ee62740-7c92-49b1-b63d-99ead2c3570d',	1010201,	4,	'0',	'0'),
          ('9487663a-1289-40d1-a77b-3c924a518d9b',	'Bank 02',	'Assets',	'BANK',	'0ee62740-7c92-49b1-b63d-99ead2c3570d',	1010202,	4,	'0',	'0'),
          ('49fed5b0-0120-4c42-a260-4b65b7c08ead',	'Investments',	'Assets',	'ASSET',	'30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	1010300,	3,	'1',	'0'),
          ('f753534c-dbb8-48f5-9078-b107d4454207',	'Shares',	'Assets',	'ASSET',	'49fed5b0-0120-4c42-a260-4b65b7c08ead',	1010301,	4,	'0',	'0'),
          ('ae096fc3-b441-4b06-bf28-e35300eff1e8',	'Fixed Deposits',	'Assets',	'ASSET',	'49fed5b0-0120-4c42-a260-4b65b7c08ead',	1010302,	4,	'0',	'0'),
          ('903dcbbd-fe7c-444d-8ba5-efbb108622c6',	'Debtors',	'Assets',	'RECEIVABLE',	'30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	1010400,	3,	'1',	'0'),
          ('2a815de1-dde2-462b-bd9d-4ee6ed747899',	'Debtor 01',	'Assets',	'ASSET',	'903dcbbd-fe7c-444d-8ba5-efbb108622c6',	1010401,	4,	'0',	'0'),
          ('4355d802-6ed9-46fc-9eb6-ae3175c5b73b',	'Debtor 02',	'Assets',	'ASSET',	'903dcbbd-fe7c-444d-8ba5-efbb108622c6',	1010402,	4,	'0',	'0'),
          ('2a183632-8c56-4d9f-ad85-c5961029e6d8',	'Retention',	'Assets',	'RECEIVABLE',	'30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	1010500,	3,	'1',	'0'),
          ('184466f8-f980-4094-a198-66e97cad9a08',	'Project 01',	'Assets',	'RECEIVABLE',	'2a183632-8c56-4d9f-ad85-c5961029e6d8',	1010501,	4,	'0',	'0'),
          ('7067c420-5d60-45f1-9321-a0056e0d8a33',	'Project 02',	'Assets',	'RECEIVABLE',	'2a183632-8c56-4d9f-ad85-c5961029e6d8',	1010502,	4,	'0',	'0'),
          ('2e0aeccf-f517-4a82-830c-cbd16d425b4b',	'Contract Receivables',	'Assets',	'RECEIVABLE',	'30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	1010600,	3,	'1',	'0'),
          ('f66cc9a6-91cc-42c4-b9b3-d33177576149',	'Project 01',	'Assets',	'RECEIVABLE',	'2e0aeccf-f517-4a82-830c-cbd16d425b4b',	1010601,	4,	'0',	'0'),
          ('6a128af0-a8e7-4c73-ba83-42948096e54a',	'Project 01',	'Assets',	'RECEIVABLE',	'2e0aeccf-f517-4a82-830c-cbd16d425b4b',	1010602,	4,	'0',	'0'),
          ('1b546d7f-5336-4cba-8ccc-b9c0c3d468c2',	'Account Receivables',	'Assets',	'RECEIVABLE',	'30cc464d-c815-4e9e-8b2d-5b7a3d26cf4e',	1010700,	3,	'1',	'0'),
          ('e08e68c7-d763-47a8-b249-8c546d37a5eb',	'Person 01',	'Assets',	'RECEIVABLE',	'1b546d7f-5336-4cba-8ccc-b9c0c3d468c2',	1010701,	4,	'0',	'0'),
          ('470f6135-11c0-483b-a172-5411cba3864d',	'Person 01',	'Assets',	'RECEIVABLE',	'1b546d7f-5336-4cba-8ccc-b9c0c3d468c2',	1010702,	4,	'0',	'0'),
          ('628a864e-70d6-428b-94c6-d1b60aa82c74',	'Inventory',	'Assets',	'INVENTORY',	'bb317393-d163-4cac-95f1-3ec204e483fe',	1020000,	2,	'1',	'0'),
          ('0d2ce09a-1d63-4847-bb46-5b46848bc10f',	'Construction Materials',	'Assets',	'INVENTORY',	'628a864e-70d6-428b-94c6-d1b60aa82c74',	1020100,	3,	'1',	'0'),
          ('8dbd37aa-7beb-43b2-95ed-76d760aec8f9',	'Project 01',	'Assets',	'INVENTORY',	'0d2ce09a-1d63-4847-bb46-5b46848bc10f',	1020101,	4,	'0',	'0'),
          ('a7025d91-e9de-432a-a1a6-7f46cd31e1a9',	'Project 02',	'Assets',	'INVENTORY',	'0d2ce09a-1d63-4847-bb46-5b46848bc10f',	1020102,	4,	'0',	'0'),
          ('f637cfa8-d796-46a9-b881-e16690d07dbd',	'Property, Plant and Equipment',	'Assets',	'ASSET',	'bb317393-d163-4cac-95f1-3ec204e483fe',	1030000,	2,	'1',	'0'),
          ('d43261bc-7c36-4db9-839f-1f32f2e6c4b1',	'Land and Buildings',	'Assets',	'ASSET',	'f637cfa8-d796-46a9-b881-e16690d07dbd',	1030100,	3,	'1',	'0'),
          ('e5e07a75-a0d1-483a-8e10-b4c6c6dd8ac6',	'Property 01',	'Assets',	'ASSET',	'd43261bc-7c36-4db9-839f-1f32f2e6c4b1',	1030101,	4,	'0',	'0'),
          ('54f7c9ce-5baf-4dd1-a1e0-4abbcd1448a9',	'Plant and Equipment',	'Assets',	'ASSET',	'f637cfa8-d796-46a9-b881-e16690d07dbd',	1030200,	3,	'1',	'0'),
          ('13c9839f-5675-4bed-b5e2-2d4499c04450',	'Equipment 01',	'Assets',	'ASSET',	'54f7c9ce-5baf-4dd1-a1e0-4abbcd1448a9',	1030201,	4,	'0',	'0'),
          ('513f75b3-e86c-484e-8cf2-8a0a3e67f552',	'Motor Vehicles',	'Assets',	'ASSET',	'f637cfa8-d796-46a9-b881-e16690d07dbd',	1030300,	3,	'1',	'0'),
          ('fc770a9b-9b96-4890-b71b-954a9a771a15',	'Vehicle 01',	'Assets',	'ASSET',	'513f75b3-e86c-484e-8cf2-8a0a3e67f552',	1030301,	4,	'0',	'0'),
          ('815e125b-9982-4a7e-9c5a-d1a2627f8df6',	'Small Tools',	'Assets',	'ASSET',	'f637cfa8-d796-46a9-b881-e16690d07dbd',	1030500,	3,	'1',	'0'),
          ('cae17cc6-115c-4855-8b05-c7ef840aedfe',	'Tools 01',	'Assets',	'ASSET',	'815e125b-9982-4a7e-9c5a-d1a2627f8df6',	1030501,	4,	'0',	'0'),
          ('7e829031-4109-4435-a157-561e20e25a83',	'Furniture and Fixtures',	'Assets',	'ASSET',	'f637cfa8-d796-46a9-b881-e16690d07dbd',	1030400,	3,	'1',	'0'),
          ('2cf1f6a1-36d7-4fbd-a0a4-51771abd1627',	'Furniture 01',	'Assets',	'ASSET',	'7e829031-4109-4435-a157-561e20e25a83',	1030401,	4,	'0',	'0'),
          ('459614d6-2d7a-477a-bd9e-297b9c7f0d80',	'Expenses',	'Expenses',	'EXPENSE',	NULL,	6000000,	1,	'1',	'0'),
          ('65699956-23b7-4547-92c6-845015104014',	'Project Expenses',	'Expenses',	'EXPENSE',	'459614d6-2d7a-477a-bd9e-297b9c7f0d80',	6010000,	2,	'1',	'0'),
          ('8f7480a1-82fa-4930-ba8d-b1e295428b99',	'Project 01',	'Expenses',	'EXPENSE',	'65699956-23b7-4547-92c6-845015104014',	6010100,	3,	'1',	'0'),
          ('b8118e69-a3d9-4ddf-8e1f-96fa5e4aed8b',	'Earth Excavation',	'Expenses',	'EXPENSE',	'8f7480a1-82fa-4930-ba8d-b1e295428b99',	6010101,	4,	'0',	'0'),
          ('9afe745c-4251-40a2-a3ef-7e83c39349f8',	'Project 02',	'Expenses',	'EXPENSE',	'65699956-23b7-4547-92c6-845015104014',	6010200,	3,	'1',	'0'),
          ('1bc49973-ceed-4754-a276-ad153bfde407',	'Mechanical Works',	'Expenses',	'EXPENSE',	'9afe745c-4251-40a2-a3ef-7e83c39349f8',	6010201,	4,	'0',	'0'),
          ('24deb124-dd79-4015-804b-1fc0849d6133',	'Electrical Works',	'Expenses',	'EXPENSE',	'9afe745c-4251-40a2-a3ef-7e83c39349f8',	6010202,	4,	'0',	'0'),
          ('fedbfbf8-49c9-42fb-a4be-8c56b9f08166',	'Precast Paving Slabs',	'Expenses',	'EXPENSE',	'9afe745c-4251-40a2-a3ef-7e83c39349f8',	6010203,	4,	'0',	'0'),
          ('79b9e32d-98ec-43f7-9b1e-265c8e43f9bd',	'Concrete works',	'Expenses',	'EXPENSE',	'9afe745c-4251-40a2-a3ef-7e83c39349f8',	6010204,	4,	'0',	'0'),
          ('39befea7-b97b-4ebd-8bb2-5290da919b61',	'Utilities',	'Expenses',	'EXPENSE',	'459614d6-2d7a-477a-bd9e-297b9c7f0d80',	6020000,	2,	'1',	'0'),
          ('1200e735-e399-4c5e-8125-2afec6b16fe2',	'Electricity',	'Expenses',	'EXPENSE',	'39befea7-b97b-4ebd-8bb2-5290da919b61',	6020001,	3,	'0',	'0'),
          ('f364ca6b-506b-428b-91f8-fd10eae49461',	'Internet',	'Expenses',	'EXPENSE',	'39befea7-b97b-4ebd-8bb2-5290da919b61',	6020002,	3,	'0',	'0'),
          ('a0a3c56a-fa5d-4694-9ab2-95333a5ba97e',	'Water',	'Expenses',	'EXPENSE',	'39befea7-b97b-4ebd-8bb2-5290da919b61',	6020003,	3,	'0',	'0'),
          ('edbcecbe-ffbe-4928-9314-bb55f93babfa',	'Taxes',	'Expenses',	'EXPENSE',	'459614d6-2d7a-477a-bd9e-297b9c7f0d80',	6030000,	2,	'1',	'0'),
          ('8692d4f1-4bbb-408e-ade6-a5c9af635ea6',	'VAT',	'Expenses',	'EXPENSE',	'edbcecbe-ffbe-4928-9314-bb55f93babfa',	6030002,	3,	'0',	'0'),
          ('0d755cb6-6ef2-4c90-81e0-c73c339a38e8',	'PAYE',	'Expenses',	'EXPENSE',	'edbcecbe-ffbe-4928-9314-bb55f93babfa',	6030001,	3,	'0',	'0'),
          ('2c88537f-1926-4c94-897c-f150036e1086',	'Income',	'Income',	'INCOME',	NULL,	4000000,	1,	'1',	'0'),
          ('b4d1eb32-8097-4ae7-b52e-a34759fd4c97',	'Interest Income',	'Income',	'INCOME',	'2c88537f-1926-4c94-897c-f150036e1086',	4010000,	2,	'1',	'0'),
          ('560a789a-45f1-427d-a510-4eb4fe137891',	'Investment Income',	'Income',	'INCOME',	'2c88537f-1926-4c94-897c-f150036e1086',	4020000,	2,	'1',	'0'),
          ('63650632-c5ab-479b-a0db-a823ee473de0',	'Sales',	'Income',	'INCOME',	'2c88537f-1926-4c94-897c-f150036e1086',	4030000,	2,	'1',	'0'),
          ('bae91601-07ab-4f0c-9c49-35ae546e4b9e',	'Rental Income',	'Income',	'INCOME',	'2c88537f-1926-4c94-897c-f150036e1086',	4040000,	2,	'1',	'0'),
          ('fcbd8dfc-b741-4a19-9980-e10b38ecdd34',	'Liability',	'Liabilities',	'LIABILITY',	NULL,	2000000,	1,	'1',	'0'),
          ('0d5bdd1f-e5a1-40d9-9603-73ee45b8205f',	'Accounts Payable',	'Liabilities',	'LIABILITY',	'fcbd8dfc-b741-4a19-9980-e10b38ecdd34',	2010000,	2,	'1',	'0'),
          ('e3fc5e76-98e1-4181-b6b4-c54ab863a8dd',	'Project 01',	'Liabilities',	'LIABILITY',	'0d5bdd1f-e5a1-40d9-9603-73ee45b8205f',	2010100,	3,	'1',	'0'),
          ('b0ef9b1c-57ff-4427-9605-18c689d3e222',	'Creditor 01',	'Liabilities',	'LIABILITY',	'e3fc5e76-98e1-4181-b6b4-c54ab863a8dd',	2010101,	4,	'0',	'0'),
          ('f1343460-06b4-46ac-839a-ee7e7865de14',	'Sub-Contractors',	'Liabilities',	'LIABILITY',	'fcbd8dfc-b741-4a19-9980-e10b38ecdd34',	2020000,	2,	'1',	'0'),
          ('6cc792ff-4805-409f-8abf-4a6e5a4c89c8',	'Project 01',	'Liabilities',	'LIABILITY',	'f1343460-06b4-46ac-839a-ee7e7865de14',	2020100,	3,	'1',	'0'),
          ('9c80331c-1dfc-4331-86d4-1bf5f32b0572',	'Sub-Contracor 01',	'Liabilities',	'LIABILITY',	'6cc792ff-4805-409f-8abf-4a6e5a4c89c8',	2020101,	4,	'0',	'0'),
          ('84e1f99f-1ebf-4791-bef4-66552ed040e5',	'Taxes',	'Liabilities',	'LIABILITY',	'fcbd8dfc-b741-4a19-9980-e10b38ecdd34',	2030000,	2,	'1',	'0'),
          ('e857c82d-23f1-43ad-b6b2-76c02ec70309',	'PAYE',	'Liabilities',	'LIABILITY',	'84e1f99f-1ebf-4791-bef4-66552ed040e5',	2030100,	3,	'1',	'0'),
          ('bc022dd9-33b6-4ff5-80e7-8ab532f79ae4',	'VAT',	'Liabilities',	'LIABILITY',	'84e1f99f-1ebf-4791-bef4-66552ed040e5',	2030200,	3,	'1',	'0'),
          ('d0859d94-0303-4fe8-a828-885fb83b7760',	'Loans',	'Liabilities',	'LIABILITY',	'fcbd8dfc-b741-4a19-9980-e10b38ecdd34',	2040000,	2,	'1',	'0'),
          ('27fda943-2eae-464a-9eee-5ed7e0d8fa69',	'Loan 01',	'Liabilities',	'LIABILITY',	'd0859d94-0303-4fe8-a828-885fb83b7760',	2040001,	3,	'0',	'0');";

      Execute.Sql(qry);
    }
  }
}