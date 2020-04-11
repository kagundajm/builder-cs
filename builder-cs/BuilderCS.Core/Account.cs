using System;

namespace BuilderCS.Core
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AccountGroup { get; set; }

        public string AccountType { get; set; }

        public Guid? ParentId { get; set; }

        public Int32 Code { get; set; }

        public byte Depth { get; set; }

        public bool IsHidden { get; set; }

        public bool IsParent { get; set; }

        // id").AsGuid().PrimaryKey()
        // ").AsAnsiString(100).Not
        // account_group").AsAnsiString
        // account_type").AsAnsiString(
        // parent_id").AsGuid().Nullabl
        // code").AsInt16().NotNullable
        // depth").AsByte().NotNullable
        // is_hidden").AsByte().NotNull
        // is_parent").AsBoolean().With
        // var id = Guid.NewGuid();
    }
}