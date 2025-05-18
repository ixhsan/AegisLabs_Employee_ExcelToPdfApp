using System.ComponentModel.DataAnnotations;

namespace AegisLabs_Employee_ExcelToPdfApp.Models
{

    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama wajib diisi")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nomor HP wajib diisi")]
        [Phone(ErrorMessage = "Format nomor telepon tidak valid")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Alamat wajib diisi")]
        public string Address { get; set; }
    }

}
