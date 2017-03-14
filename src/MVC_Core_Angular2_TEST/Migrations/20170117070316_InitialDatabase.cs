using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACME.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Products",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Products",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");
        }
    }
}
