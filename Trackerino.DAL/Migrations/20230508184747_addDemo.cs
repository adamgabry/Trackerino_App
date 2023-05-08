using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trackerino.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addDemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0953f3ce-7b1a-48c1-9796-d2bac7f67868"), "Project seed" },
                    { new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), "Project seed" },
                    { new Guid("229ec7a1-58bf-4269-8a82-ec741a5abfac"), "Project seed" },
                    { new Guid("4fd824c0-a7d1-48ba-8e7c-4f136cf8bf31"), "Project seed" },
                    { new Guid("5dca4cea-b8a8-4c86-a0b3-ffb78fba1a09"), "Project seed" },
                    { new Guid("98b7f7b6-0f51-43b3-b8c0-b5fcfff6dc2e"), "Project seed" },
                    { new Guid("f78ed923-e094-4016-9045-3f5bb7f2eb88"), "Project seed" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ImageUrl", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("3fada1af-3777-43b8-817b-b796005bdc43"), null, "User1 seed Name ", "User1 seed Surname" },
                    { new Guid("77146ea0-2d86-4874-b75e-fba628afc698"), "https://static.wikia.nocookie.net/mrrobot/images/3/3e/Elliot.jpg/revision/latest?cb=20150810201239", "Eliot", "Anderson" },
                    { new Guid("88db5c3b-bd8d-439a-ba86-b7dd75735185"), "https://static.wikia.nocookie.net/mrrobot/images/3/3e/Elliot.jpg/revision/latest?cb=20150810201239", "Eliot", "Anderson" },
                    { new Guid("a4ead4ba-e1ec-43d9-b6a8-38dd8ef70bf2"), "https://static.wikia.nocookie.net/mrrobot/images/3/3e/Elliot.jpg/revision/latest?cb=20150810201239", "Eliot", "Anderson" },
                    { new Guid("b86d6d89-e319-44ca-a5b5-03f3b61c6079"), null, "User2 seed Name ", "User2 seed Surname" },
                    { new Guid("c6128de8-a1a1-45fc-a777-4e6cf056ebb0"), "https://static.wikia.nocookie.net/mrrobot/images/3/3e/Elliot.jpg/revision/latest?cb=20150810201239", "Eliot", "Anderson" },
                    { new Guid("e1a7b31f-0223-4b1b-b8ad-4aaa627908b9"), "https://static.wikia.nocookie.net/mrrobot/images/3/3e/Elliot.jpg/revision/latest?cb=20150810201239", "Eliot", "Anderson" }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Description", "EndDateTime", "ProjectId", "StartDateTime", "Tag", "UserId" },
                values: new object[,]
                {
                    { new Guid("143332b9-080e-4953-aea5-bef64679b052"), "Hour long meeting", new DateTime(2023, 1, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), new DateTime(2023, 1, 24, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("c6128de8-a1a1-45fc-a777-4e6cf056ebb0") },
                    { new Guid("274d0cc9-a948-4818-aadb-a8b4c0506619"), "Hour long meeting", new DateTime(2023, 1, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), new DateTime(2023, 1, 24, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("c6128de8-a1a1-45fc-a777-4e6cf056ebb0") },
                    { new Guid("4fa608f9-77d2-498b-a6c1-387fda3dfb3d"), "Hour long meeting", new DateTime(2023, 1, 24, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), new DateTime(2023, 1, 24, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("c6128de8-a1a1-45fc-a777-4e6cf056ebb0") },
                    { new Guid("f7edb698-5130-4fcb-8f49-c1ac22dacfe8"), "Seeded Activity 2 description", new DateTime(2023, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), new DateTime(2023, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("b86d6d89-e319-44ca-a5b5-03f3b61c6079") },
                    { new Guid("fccccd03-fa27-46d8-950d-7a5d9fd1b103"), "Seeded Activity 1 description", new DateTime(2023, 12, 24, 22, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), new DateTime(2023, 12, 24, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("3fada1af-3777-43b8-817b-b796005bdc43") }
                });

            migrationBuilder.InsertData(
                table: "UserProject",
                columns: new[] { "Id", "ProjectId", "UserId" },
                values: new object[,]
                {
                    { new Guid("30872eff-ced4-4f2b-89db-0ee83a74d279"), new Guid("f78ed923-e094-4016-9045-3f5bb7f2eb88"), new Guid("3fada1af-3777-43b8-817b-b796005bdc43") },
                    { new Guid("58f19566-686f-4093-a8a6-77b20ea10863"), new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), new Guid("3fada1af-3777-43b8-817b-b796005bdc43") },
                    { new Guid("87833e66-05ba-4d6b-900b-fe5ace88dbd8"), new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"), new Guid("b86d6d89-e319-44ca-a5b5-03f3b61c6079") },
                    { new Guid("a2e6849d-a158-4436-980c-7fc26b60c674"), new Guid("4fd824c0-a7d1-48ba-8e7c-4f136cf8bf31"), new Guid("3fada1af-3777-43b8-817b-b796005bdc43") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("143332b9-080e-4953-aea5-bef64679b052"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("274d0cc9-a948-4818-aadb-a8b4c0506619"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("4fa608f9-77d2-498b-a6c1-387fda3dfb3d"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("f7edb698-5130-4fcb-8f49-c1ac22dacfe8"));

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: new Guid("fccccd03-fa27-46d8-950d-7a5d9fd1b103"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("0953f3ce-7b1a-48c1-9796-d2bac7f67868"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("229ec7a1-58bf-4269-8a82-ec741a5abfac"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("5dca4cea-b8a8-4c86-a0b3-ffb78fba1a09"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("98b7f7b6-0f51-43b3-b8c0-b5fcfff6dc2e"));

            migrationBuilder.DeleteData(
                table: "UserProject",
                keyColumn: "Id",
                keyValue: new Guid("30872eff-ced4-4f2b-89db-0ee83a74d279"));

            migrationBuilder.DeleteData(
                table: "UserProject",
                keyColumn: "Id",
                keyValue: new Guid("58f19566-686f-4093-a8a6-77b20ea10863"));

            migrationBuilder.DeleteData(
                table: "UserProject",
                keyColumn: "Id",
                keyValue: new Guid("87833e66-05ba-4d6b-900b-fe5ace88dbd8"));

            migrationBuilder.DeleteData(
                table: "UserProject",
                keyColumn: "Id",
                keyValue: new Guid("a2e6849d-a158-4436-980c-7fc26b60c674"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("77146ea0-2d86-4874-b75e-fba628afc698"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88db5c3b-bd8d-439a-ba86-b7dd75735185"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a4ead4ba-e1ec-43d9-b6a8-38dd8ef70bf2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e1a7b31f-0223-4b1b-b8ad-4aaa627908b9"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("0d7d53ae-d631-4daa-8c71-c3370e69a16b"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("4fd824c0-a7d1-48ba-8e7c-4f136cf8bf31"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("f78ed923-e094-4016-9045-3f5bb7f2eb88"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3fada1af-3777-43b8-817b-b796005bdc43"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b86d6d89-e319-44ca-a5b5-03f3b61c6079"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c6128de8-a1a1-45fc-a777-4e6cf056ebb0"));
        }
    }
}
