using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MultiProject.Delivery.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "unique_units_details",
                columns: table => new
                {
                    uniqueunitid = table.Column<int>(name: "unique_unit_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barcode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    transportunitid = table.Column<int>(name: "transport_unit_id", type: "integer", nullable: false, comment: "id from table transport_units")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unique_units_details", x => x.uniqueunitid);
                });

            migrationBuilder.CreateTable(
                name: "units_of_measure",
                columns: table => new
                {
                    unitofmeasureid = table.Column<int>(name: "unit_of_measure_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    symbol = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false, comment: "symbol that will be presented to users eg. Kg"),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_units_of_measure", x => x.unitofmeasureid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, comment: "Is user active or inactive, inactive users cannot log in, and be assigned to new deliveries."),
                    role = table.Column<int>(type: "integer", nullable: false, comment: "What roles has user assigned (bit field) 0 - no role, 1 - Deliverer, 2 - Manager"),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "username and login"),
                    password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "password hash"),
                    phonenumber = table.Column<string>(name: "phone_number", type: "character varying(15)", maxLength: 15, nullable: false, comment: "user phone number without whitespaces or separators"),
                    geolocationlastupdate = table.Column<DateTime>(name: "geolocation_last_update", type: "timestamp with time zone", nullable: true, comment: "last date of geolocation update"),
                    geolocationheading = table.Column<double>(name: "geolocation_heading", type: "double precision", precision: 4, scale: 2, nullable: true, comment: "user last known heading in degrees where 0 is north"),
                    geolocationspeed = table.Column<double>(name: "geolocation_speed", type: "double precision", precision: 4, scale: 2, nullable: true, comment: "user last known speed in m/s"),
                    geolocationlatitude = table.Column<double>(name: "geolocation_latitude", type: "double precision", precision: 3, scale: 5, nullable: true, comment: "user last known latitude, with precision up to 1m"),
                    geolocationlongitude = table.Column<double>(name: "geolocation_longitude", type: "double precision", precision: 3, scale: 5, nullable: true, comment: "user last known longitude, with precision up to 1m"),
                    geolocationaccuracy = table.Column<double>(name: "geolocation_accuracy", type: "double precision", precision: 3, scale: 0, nullable: true, comment: "level of accuracy of longitude and latitude in meters. Can be null if speed is 0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "multi_units_details",
                columns: table => new
                {
                    multiunitid = table.Column<int>(name: "multi_unit_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    unitofmeasureid = table.Column<int>(name: "unit_of_measure_id", type: "integer", nullable: false, comment: "id from table units_of_measure"),
                    amount = table.Column<double>(type: "double precision", precision: 5, scale: 3, nullable: false, comment: "amount to be delivered, depends on type of unit of measure for example it can be pieces/kilograms/meters etc.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_multi_units_details", x => x.multiunitid);
                    table.ForeignKey(
                        name: "FK_multi_units_details_units_of_measure_unit_of_measure_id",
                        column: x => x.unitofmeasureid,
                        principalTable: "units_of_measure",
                        principalColumn: "unit_of_measure_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transports",
                columns: table => new
                {
                    transportid = table.Column<int>(name: "transport_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    delivererid = table.Column<Guid>(name: "deliverer_id", type: "uuid", nullable: false, comment: "id from table users. This user will deliver units to recipients and unload them"),
                    status = table.Column<int>(type: "integer", nullable: false, comment: "Transport status 0 - New, 1 - Processing, 2 - Finished, 3 - Deleted"),
                    number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    additionalinformation = table.Column<string>(name: "additional_information", type: "character varying(2000)", maxLength: 2000, nullable: true),
                    totalweight = table.Column<double>(name: "total_weight", type: "double precision", precision: 9, scale: 4, nullable: true),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "timestamp with time zone", nullable: false),
                    managerid = table.Column<Guid>(name: "manager_id", type: "uuid", nullable: false, comment: "id from table users. This user is responsible for this transport, and a contact person for deliverer in case of any problems")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transports", x => x.transportid);
                    table.ForeignKey(
                        name: "FK_transports_users_deliverer_id",
                        column: x => x.delivererid,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transports_users_manager_id",
                        column: x => x.managerid,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transport_units",
                columns: table => new
                {
                    transportunitid = table.Column<int>(name: "transport_unit_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transportid = table.Column<int>(name: "transport_id", type: "integer", nullable: false),
                    number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false, comment: "Transport unit status 1 - New, 2 - PartiallyDelivered, 3 - Delivered, 4 - Deleted"),
                    additionalinformation = table.Column<string>(name: "additional_information", type: "character varying(2000)", maxLength: 2000, nullable: true),
                    recipientcompanyname = table.Column<string>(name: "recipient_company_name", type: "character varying(200)", maxLength: 200, nullable: true),
                    recipientname = table.Column<string>(name: "recipient_name", type: "character varying(200)", maxLength: 200, nullable: true),
                    recipientlastname = table.Column<string>(name: "recipient_last_name", type: "character varying(200)", maxLength: 200, nullable: true),
                    recipientphonenumber = table.Column<string>(name: "recipient_phone_number", type: "character varying(15)", maxLength: 15, nullable: false, comment: "phone number without whitespaces or separators"),
                    recipientflatnumber = table.Column<string>(name: "recipient_flat_number", type: "character varying(5)", maxLength: 5, nullable: true),
                    recipientstreetnumber = table.Column<string>(name: "recipient_street_number", type: "character varying(5)", maxLength: 5, nullable: false),
                    recipientstreet = table.Column<string>(name: "recipient_street", type: "character varying(200)", maxLength: 200, nullable: true),
                    recipienttown = table.Column<string>(name: "recipient_town", type: "character varying(200)", maxLength: 200, nullable: false),
                    recipientcountry = table.Column<string>(name: "recipient_country", type: "character varying(200)", maxLength: 200, nullable: false),
                    recipientpostcode = table.Column<string>(name: "recipient_post_code", type: "character varying(200)", maxLength: 200, nullable: false),
                    uniqueunitid = table.Column<int>(name: "unique_unit_id", type: "integer", nullable: true),
                    multiunitid = table.Column<int>(name: "multi_unit_id", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport_units", x => x.transportunitid);
                    table.ForeignKey(
                        name: "FK_transport_units_multi_units_details_multi_unit_id",
                        column: x => x.multiunitid,
                        principalTable: "multi_units_details",
                        principalColumn: "multi_unit_id");
                    table.ForeignKey(
                        name: "FK_transport_units_transports_transport_id",
                        column: x => x.transportid,
                        principalTable: "transports",
                        principalColumn: "transport_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transport_units_unique_units_details_unique_unit_id",
                        column: x => x.uniqueunitid,
                        principalTable: "unique_units_details",
                        principalColumn: "unique_unit_id");
                });

            migrationBuilder.CreateTable(
                name: "scans",
                columns: table => new
                {
                    scanid = table.Column<int>(name: "scan_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transportunitid = table.Column<int>(name: "transport_unit_id", type: "integer", nullable: false, comment: "id from table transport_units"),
                    status = table.Column<int>(type: "integer", nullable: false, comment: "scan status 0 - Valid, 1 - Deleted"),
                    lastupdatedate = table.Column<DateTime>(name: "last_update_date", type: "timestamp with time zone", nullable: false),
                    delivererid = table.Column<Guid>(name: "deliverer_id", type: "uuid", nullable: false, comment: "id from table users. Designates who created this scan"),
                    quantity = table.Column<double>(type: "double precision", precision: 8, scale: 3, nullable: true, comment: "unloaded quantity from multi_unit, null for unique_unit type of transport unit delivery"),
                    locationlatitude = table.Column<double>(name: "location_latitude", type: "double precision", precision: 3, scale: 5, nullable: true, comment: "Latitude of scan, with precision up to 1m"),
                    locationlongitude = table.Column<double>(name: "location_longitude", type: "double precision", precision: 3, scale: 5, nullable: true, comment: "Longitude of scan, with precision up to 1m"),
                    locationaccuracy = table.Column<double>(name: "location_accuracy", type: "double precision", precision: 3, scale: 0, nullable: true, comment: "level of accuracy for longitude and latitude in meters")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scans", x => x.scanid);
                    table.ForeignKey(
                        name: "FK_scans_transport_units_transport_unit_id",
                        column: x => x.transportunitid,
                        principalTable: "transport_units",
                        principalColumn: "transport_unit_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scans_users_deliverer_id",
                        column: x => x.delivererid,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    attachmentid = table.Column<int>(name: "attachment_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creatorid = table.Column<Guid>(name: "creator_id", type: "uuid", nullable: false, comment: "id from table users"),
                    transportid = table.Column<int>(name: "transport_id", type: "integer", nullable: false, comment: "id from table transports"),
                    scanid = table.Column<int>(name: "scan_id", type: "integer", nullable: true, comment: "id from table scans"),
                    transportunitid = table.Column<int>(name: "transport_unit_id", type: "integer", nullable: true, comment: "id from table transport_units"),
                    status = table.Column<int>(type: "integer", nullable: false, comment: "0 - Valid, 1 - Deleted"),
                    lastupdate = table.Column<DateTime>(name: "last_update", type: "timestamp with time zone", nullable: false),
                    payload = table.Column<byte[]>(type: "bytea", nullable: true),
                    additionalinformation = table.Column<string>(name: "additional_information", type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => x.attachmentid);
                    table.ForeignKey(
                        name: "FK_attachments_scans_scan_id",
                        column: x => x.scanid,
                        principalTable: "scans",
                        principalColumn: "scan_id");
                    table.ForeignKey(
                        name: "FK_attachments_transport_units_transport_unit_id",
                        column: x => x.transportunitid,
                        principalTable: "transport_units",
                        principalColumn: "transport_unit_id");
                    table.ForeignKey(
                        name: "FK_attachments_transports_transport_id",
                        column: x => x.transportid,
                        principalTable: "transports",
                        principalColumn: "transport_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_attachments_users_creator_id",
                        column: x => x.creatorid,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attachments_creator_id",
                table: "attachments",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_scan_id",
                table: "attachments",
                column: "scan_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_transport_id",
                table: "attachments",
                column: "transport_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_transport_unit_id",
                table: "attachments",
                column: "transport_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_multi_units_details_unit_of_measure_id",
                table: "multi_units_details",
                column: "unit_of_measure_id");

            migrationBuilder.CreateIndex(
                name: "IX_scans_deliverer_id",
                table: "scans",
                column: "deliverer_id");

            migrationBuilder.CreateIndex(
                name: "IX_scans_transport_unit_id",
                table: "scans",
                column: "transport_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_transport_units_multi_unit_id",
                table: "transport_units",
                column: "multi_unit_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transport_units_transport_id",
                table: "transport_units",
                column: "transport_id");

            migrationBuilder.CreateIndex(
                name: "IX_transport_units_unique_unit_id",
                table: "transport_units",
                column: "unique_unit_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transports_deliverer_id",
                table: "transports",
                column: "deliverer_id");

            migrationBuilder.CreateIndex(
                name: "IX_transports_manager_id",
                table: "transports",
                column: "manager_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "scans");

            migrationBuilder.DropTable(
                name: "transport_units");

            migrationBuilder.DropTable(
                name: "multi_units_details");

            migrationBuilder.DropTable(
                name: "transports");

            migrationBuilder.DropTable(
                name: "unique_units_details");

            migrationBuilder.DropTable(
                name: "units_of_measure");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
