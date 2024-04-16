using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetData_Lb3.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_doctor_id",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_patient_id",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscounts_Doctors_doctorID",
                table: "PatientDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscounts_Patients_patientID",
                table: "PatientDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Doctors_doctor_id",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "patients");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "doctors");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "appointments");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "doctor_schedule");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_patient_id",
                table: "appointments",
                newName: "IX_appointments_patient_id");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_doctor_id",
                table: "appointments",
                newName: "IX_appointments_doctor_id");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_doctor_id",
                table: "doctor_schedule",
                newName: "IX_doctor_schedule_doctor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_patients",
                table: "patients",
                column: "patient_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_doctors",
                table: "doctors",
                column: "doctor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appointments",
                table: "appointments",
                column: "appointment_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_doctor_schedule",
                table: "doctor_schedule",
                column: "schedule_id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_doctors_doctor_id",
                table: "appointments",
                column: "doctor_id",
                principalTable: "doctors",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patients_patient_id",
                table: "appointments",
                column: "patient_id",
                principalTable: "patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_doctor_schedule_doctors_doctor_id",
                table: "doctor_schedule",
                column: "doctor_id",
                principalTable: "doctors",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscounts_doctors_doctorID",
                table: "PatientDiscounts",
                column: "doctorID",
                principalTable: "doctors",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscounts_patients_patientID",
                table: "PatientDiscounts",
                column: "patientID",
                principalTable: "patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_doctors_doctor_id",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patients_patient_id",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_doctor_schedule_doctors_doctor_id",
                table: "doctor_schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscounts_doctors_doctorID",
                table: "PatientDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscounts_patients_patientID",
                table: "PatientDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_patients",
                table: "patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_doctors",
                table: "doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appointments",
                table: "appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_doctor_schedule",
                table: "doctor_schedule");

            migrationBuilder.RenameTable(
                name: "patients",
                newName: "Patients");

            migrationBuilder.RenameTable(
                name: "doctors",
                newName: "Doctors");

            migrationBuilder.RenameTable(
                name: "appointments",
                newName: "Appointments");

            migrationBuilder.RenameTable(
                name: "doctor_schedule",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_patient_id",
                table: "Appointments",
                newName: "IX_Appointments_patient_id");

            migrationBuilder.RenameIndex(
                name: "IX_appointments_doctor_id",
                table: "Appointments",
                newName: "IX_Appointments_doctor_id");

            migrationBuilder.RenameIndex(
                name: "IX_doctor_schedule_doctor_id",
                table: "Schedules",
                newName: "IX_Schedules_doctor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "patient_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "doctor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "appointment_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "schedule_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_doctor_id",
                table: "Appointments",
                column: "doctor_id",
                principalTable: "Doctors",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_patient_id",
                table: "Appointments",
                column: "patient_id",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscounts_Doctors_doctorID",
                table: "PatientDiscounts",
                column: "doctorID",
                principalTable: "Doctors",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscounts_Patients_patientID",
                table: "PatientDiscounts",
                column: "patientID",
                principalTable: "Patients",
                principalColumn: "patient_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Doctors_doctor_id",
                table: "Schedules",
                column: "doctor_id",
                principalTable: "Doctors",
                principalColumn: "doctor_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
