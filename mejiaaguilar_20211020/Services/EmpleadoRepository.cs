using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mejiaaguilar_20211020.SQL.Context;
using mejiaaguilar_20211020.Models;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.Entity;

namespace mejiaaguilar_20211020.Services
{
    public class EmpleadoRepository
    {

        readonly mejiaaguilar_20211020Entities db = new mejiaaguilar_20211020Entities();

        public EmpleadoVM getListEmp(int page)
        {
            EmpleadoVM model = new EmpleadoVM { lstEmpleados = new List<EmpleadoVM>() };
            var lst = db.fnPager(page, 2);
            foreach(var item in lst){
                model.lstEmpleados.Add(new EmpleadoVM
                {
                    idEmp = (int)item.IdEmp,
                    name = item.Name,
                    lastname = item.LastName,
                    DeptName = item.DeptName,
                    fechaingreso = (DateTime)item.fechaIngreso
                });
            }
            model.TotalReg = db.Empleados.Where(x => x.Status == 1).Count();
            model.RegPerPage = 2;
            model.Page = page + 1;
            return model;
        }

        public EmpleadoVM getEmpleado(int id)
        {
            var DataEmp = db.Empleados.Where(x => x.idEmp == id)
                .Include("Departamentos")
                .Single();
            EmpleadoVM model = new EmpleadoVM
            {
                idEmp = DataEmp.idEmp,
                name = DataEmp.Name,
                lastname = DataEmp.LastName,
                address = DataEmp.Address,
                telefono = DataEmp.Telefono,
                DeptName = DataEmp.Departamentos.Name,
                fechaingreso = (DateTime)DataEmp.fechaIngreso,
                salary = DataEmp.Salary
            };

            return model;
        }

        public bool AddEmpleado(EmpleadoVM model)
        {

            try
            {
                Empleados emp = new Empleados
                {
                    Name = model.name,
                    LastName = model.lastname,
                    Address = model.address,
                    Telefono = model.telefono,
                    idDept = model.idDept,
                    Salary = model.salary,
                    fechaIngreso = DateTime.Today,
                    Status = 1
                };
                db.Empleados.Add(emp);
                db.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                Debug.WriteLine("Exception Message: " + ex.Message);
                return false;
            }
        }

        public bool EditEmpleado(EmpleadoVM model)
        {

            try
            {
                Empleados emp = new Empleados
                {
                    idEmp = model.idEmp,
                    Name = model.name,
                    LastName = model.lastname,
                    Address = model.address,
                    Telefono = model.telefono,
                    idDept = model.idDept,
                    Salary = model.salary,
                    fechaIngreso = DateTime.Today,
                    Status = 1
                };
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message: " + ex.Message);
                return false;
            }
        }

        public bool DeleteEmpleado(int idEmp)
        {

            try
            {
                Empleados emp = db.Empleados.Where(x => x.idEmp == idEmp).Single();
                emp.Status = 0;
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message: " + ex.Message);
                return false;
            }
        }

        public List<SelectListItem> getDepartamentos()
        {
            List<SelectListItem> Lst = new List<SelectListItem>();
            var lstado = db.Departamentos.ToList();
            foreach(var item in lstado)
            {
                Lst.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.idDept.ToString()
                });
            }

            return Lst;
        }

    }
}