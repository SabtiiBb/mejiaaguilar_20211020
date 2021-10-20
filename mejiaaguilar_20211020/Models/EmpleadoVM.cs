using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mejiaaguilar_20211020.Models
{
    public class EmpleadoVM : BaseModel
    {
        public int idEmp { get; set; }
        [Required(ErrorMessage = "El nombre de empleado es requerido!")]
        public string name { get; set; }
        [Required(ErrorMessage = "El apellido del empleado es requerido!")]
        public string lastname { get; set; }
        [Required(ErrorMessage = "La direccion del empleado es requerida!")]
        public string address { get; set; }
        public string DeptName { get; set; }
        [Required(ErrorMessage = "El telefono es requerido!")]
        [RegularExpression(@"(\+503|00503|503)?(| |-){1}(2|6|7)([0-9]){3}(| |-){1}([0-9]){4}", ErrorMessage = "¡El número ingresado no es válido!")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Seleccione un Departamento!")]
        public int idDept { get; set; }
        [Required(ErrorMessage = "Ingrese un salario para el empleado")]
        public decimal salary { get; set; }
        [Required(ErrorMessage = "La fecha de ingreso es requerida!")]
        [DataType(DataType.Date)]
        public DateTime fechaingreso { get; set; }

        public List<EmpleadoVM> lstEmpleados { get; set; }
    }
}