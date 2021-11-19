using ApiTienda.Models;
using ApiTienda.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Controllers
{
    public class HomeController : Controller
    {
        BD DataBaseInsert = new BD();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public List<cliente> getCliente()
        {

            List<cliente> listaCliente = new List<cliente>();
            List<factura> listaFactura = new List<factura>();
            List<detalles_factura> listadetallesFacturas = new List<detalles_factura>();
            
            cliente cliente = new cliente();

            cliente.id_cliente = 1;
            cliente.nombre = "Sebastian";
            cliente.apellido = "Mira Castro";
            cliente.documento = 1097720244;

            listaCliente.Add(cliente);

            factura factura1 = new factura();

            factura1.id_invoice = 1;
            factura1.id_cliente = 1;
            factura1.codigo = 204721;

            listaFactura.Add(factura1);

            detalles_factura detalles = new detalles_factura();

            detalles.id_detalles = 1;
            detalles.id_invoice = 1;
            detalles.descripcion = "SmartPhone";
            detalles.valor = "2.343.657";

            listadetallesFacturas.Add(detalles);

            //cliente.ListaFactura = listaFactura;
            factura1.ListaDetalles = listadetallesFacturas;

            return listaCliente;
        } 
        public string InsertClientInDataBase([FromBody] cliente Insert)
        {
            string ExecuteInsert = "Insert into client (id, Name, Last_Name, Document_ID) values ( " + Insert.id_cliente + ", '" + Insert.nombre + "' , '" + Insert.apellido + "', " + Insert.documento + " );";

            string resultado = DataBaseInsert.ejecutarSQL(ExecuteInsert);

            return resultado;
        }
        public string InsertDetailsAndInvoiceInDataBase([FromBody] factura InsertInvoice)
        {
            string sql = "Insert into invoice (ID_Client, Cod) values (" + InsertInvoice.id_cliente + ", " + InsertInvoice.codigo +");" + Environment.NewLine;

            sql += "select @@identity as ID;" + Environment.NewLine;

            foreach (detalles_factura Values in InsertInvoice.ListaDetalles)
            {
                sql += "Insert into invoicedetail (ID_Invoice, Description, Value) values (@@identity, '" + Values.descripcion + "', '" + Values.valor + "');";
            }
            string resultado = DataBaseInsert.ejecutarSQL(sql);

            return resultado;
        }

        public List<cliente> SeeClients()
        {
            List<cliente> ClientList = new List<cliente>();

            DataTable TData = DataBaseInsert.GetElementInDataBase($"select * from client");

            ClientList = (from DataRow data in TData.Rows
                          select new cliente()
                          {
                              id_cliente = Convert.ToInt32(data["id"]),
                              nombre = Convert.ToString(data["Name"]),
                              apellido = Convert.ToString(data["Last_Name"]),
                              documento = Convert.ToInt32(data["Document_ID"])
                          }).ToList();
            return ClientList;

        }

        public List<cliente> SeeOneClient(int id)
        {
            List<cliente> Client = new List<cliente>();

            DataTable DataT = DataBaseInsert.GetElementInDataBase($"select * from client where id = {id}");

            Client = (from DataRow data in DataT.Rows
                      select new cliente()
                      {
                          id_cliente = Convert.ToInt32(data["id"]),
                          nombre = Convert.ToString(data["Name"]),
                          apellido = Convert.ToString(data["Last_Name"]),
                          documento = Convert.ToInt32(data["Document_ID"])
                      }).ToList();
            return Client;
        }

        public List<factura> SeeBillsAndDetails(int id)
        {
            List<factura> BillsList = new List<factura>();
            List<detalles_factura> BillsListsDetails = new List<detalles_factura>();

            detalles_factura BillsDetails = new detalles_factura();

            DataTable BillsData = DataBaseInsert.GetElementInDataBase($"select * from invoice where id_invoice = {id}");
            DataTable BillsDataDetail = DataBaseInsert.GetElementInDataBase($"select * from invoicedetail");

            BillsListsDetails = (from DataRow data in BillsDataDetail.Rows
                                 select new detalles_factura
                                 {
                                     id_detalles = Convert.ToInt32(data["ID_Detail"]),
                                     id_invoice = Convert.ToInt32(data["ID_Invoice"]),
                                     descripcion = Convert.ToString(data["Description"]),
                                     valor = Convert.ToString(data["Value"])
                                 }).ToList();

            BillsList = (from DataRow data in BillsData.Rows
                         select new factura
                         {
                             id_invoice = Convert.ToInt32(data["id_invoice"]),
                             id_cliente = Convert.ToInt32(data["ID_Client"]),
                             codigo = Convert.ToInt32(data["Cod"]),
                             ListaDetalles = BillsListsDetails
                         }).ToList();
            return BillsList;
        }

        public List<cliente> insertarlista([FromBody] List<cliente> listaclientes)
        {
            return listaclientes;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
