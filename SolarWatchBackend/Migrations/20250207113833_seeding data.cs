using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SolarWatch.Migrations
{
    /// <inheritdoc />
    public partial class seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, 40.509999999999998, 2.1899999999999999, "Barcelona" },
                    { 2, 48.850000000000001, 2.3500000000000001, "Paris" },
                    { 3, 51.509999999999998, -0.13, "London" },
                    { 4, 40.710000000000001, -74.010000000000005, "New York" },
                    { 5, 34.049999999999997, -118.23999999999999, "Los Angeles" },
                    { 6, 41.899999999999999, 12.49, "Rome" },
                    { 7, 35.68, 139.69, "Tokyo" },
                    { 8, 55.75, 37.619999999999997, "Moscow" },
                    { 9, 52.520000000000003, 13.4, "Berlin" },
                    { 10, 37.770000000000003, -122.42, "San Francisco" },
                    { 11, 39.899999999999999, 116.40000000000001, "Beijing" },
                    { 12, 19.43, -99.129999999999995, "Mexico City" },
                    { 13, 28.609999999999999, 77.230000000000004, "New Delhi" },
                    { 14, -33.869999999999997, 151.21000000000001, "Sydney" },
                    { 15, 1.3500000000000001, 103.81999999999999, "Singapore" },
                    { 16, 50.079999999999998, 14.43, "Prague" },
                    { 17, 59.329999999999998, 18.059999999999999, "Stockholm" },
                    { 18, 31.23, 121.47, "Shanghai" },
                    { 19, -23.550000000000001, -46.630000000000003, "São Paulo" },
                    { 20, 6.5199999999999996, 3.3700000000000001, "Lagos" },
                    { 21, 43.649999999999999, -79.379999999999995, "Toronto" },
                    { 22, 55.68, 12.57, "Copenhagen" },
                    { 23, 25.280000000000001, 51.530000000000001, "Doha" },
                    { 24, -26.199999999999999, 28.039999999999999, "Johannesburg" },
                    { 25, 35.689999999999998, 51.420000000000002, "Tehran" }
                });

            migrationBuilder.InsertData(
                table: "Solars",
                columns: new[] { "Id", "CityId", "Date", "Sunrise", "Sunset" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2024, 2, 7), "06:30", "18:45" },
                    { 2, 2, new DateOnly(2024, 2, 7), "07:15", "19:05" },
                    { 3, 3, new DateOnly(2024, 2, 7), "07:45", "18:30" },
                    { 4, 4, new DateOnly(2024, 2, 7), "06:50", "17:55" },
                    { 5, 5, new DateOnly(2024, 2, 7), "06:55", "18:20" },
                    { 6, 6, new DateOnly(2024, 2, 7), "07:10", "18:40" },
                    { 7, 7, new DateOnly(2024, 2, 7), "06:20", "17:30" },
                    { 8, 8, new DateOnly(2024, 2, 7), "08:15", "16:45" },
                    { 9, 9, new DateOnly(2024, 2, 7), "07:50", "17:20" },
                    { 10, 10, new DateOnly(2024, 2, 7), "06:40", "17:50" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Solars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
