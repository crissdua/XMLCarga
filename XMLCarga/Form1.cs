using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace XMLCarga
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Variables
        int ContD = 0;
        string archivo;
        Stream myStream = null;
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            parsersEncabezado();
        }


        public void parsersEncabezado()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "xml files (*.xml)|";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            System.IO.StreamWriter file = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Log.txt", true);
                            archivo = openFileDialog1.SafeFileName.ToString();
                            XmlDocument xml = new XmlDocument();
                            xml.Load(myStream);
                            file.WriteLine("Archivo Cargado: " + archivo);
                            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            //--------------------------------------------------------------------- INFOTRIBUTARIA ----------------------------------------------------------------------------------
                            #region infotributaria
                            XmlNodeList xnInfotributaria = xml.SelectNodes("factura/infoTributaria");
                            foreach (XmlNode xn in xnInfotributaria)
                            {
                                    string razonsocial = xn["razonSocial"].InnerText;
                                    string ruc = xn["ruc"].InnerText;
                                    string cardoc = xn["codDoc"].InnerText;
                                    string estab = xn["estab"].InnerText;
                                    string ptoemi = xn["ptoEmi"].InnerText;
                                    string secuencial = xn["secuencial"].InnerText;
                                    string dirmatriz = xn["dirMatriz"].InnerText;
                                    listBox1.Items.Add("InfoTributaria Ingresado Correctamente Funico:  " );
                            }
                            #endregion
                            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            //------------------------------------------------------------------------ INFOFACTURA ----------------------------------------------------------------------------------
                            #region Infofactura 
                            XmlNodeList xnInfofactura = xml.SelectNodes("factura/infoFactura");
                            foreach (XmlNode x in xnInfofactura)
                            {
                                    string fechaemision = x["fechaEmision"].InnerText;
                                    string direstablecimiento = x["dirEstablecimiento"].InnerText;
                                    string contribuyenteespecial = x["contribuyenteEspecial"].InnerText;
                                    string obligadocontabilidad = x["obligadoContabilidad"].InnerText;
                                    string tipoidentificacioncomprador = x["tipoIdentificacionComprador"].InnerText;
                                    string razonsocialcomprador = x["razonSocialComprador"].InnerText;
                                    string identificacionComprador = x["identificacionComprador"].InnerText;
                                    string totalSinImpuestos = x["totalSinImpuestos"].InnerText;
                                    string totalDescuento = x["totalDescuento"].InnerText;
                                    string codDocReembolso = x["codDocReembolso"].InnerText;
                                    string totalComprobantesReembolso  = x["totalComprobantesReembolso"].InnerText;
                                    string totalBaseImponibleReembolso = x["totalBaseImponibleReembolso"].InnerText;
                                    string totalImpuestoReembolso = x["totalImpuestoReembolso"].InnerText;

                                    XmlNodeList xnListimpuesto = xml.SelectNodes("factura/infoFactura/totalConImpuestos/totalImpuesto");
                          
                                    foreach (XmlNode xn in xnListimpuesto)
                                    {
                                            string codigo = xn["codigo"].InnerText;
                                            string codigoPorcentaje = xn["codigoPorcentaje"].InnerText;
                                            string baseImponible = xn["baseImponible"].InnerText;
                                            string tarifa = xn["tarifa"].InnerText;
                                            string valor = xn["valor"].InnerText;
                                    }
                                    string propina = x["propina"].InnerText;
                                    string importeTotal = x["importeTotal"].InnerText;
                                    string moneda = x["moneda"].InnerText;

                                    XmlNodeList xnListpagos = xml.SelectNodes("factura/infoFactura/pagos/pago");

                                    foreach (XmlNode xn in xnListpagos)
                                    {
                                        //string fecha = xn["formapago"].InnerText;
                                        string total = xn["total"].InnerText;
                                        string plazo = xn["plazo"].InnerText;
                                        string unidadTiempo = xn["unidadTiempo"].InnerText;
                                    }

                                    //Termina Barrido de XML DETALLE
                                    listBox1.Items.Add("ifoFactura Ingresado Correctamente: ");
                                    ContD++;
                                }

                                #endregion

                                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                //------------------------------------------------------------------------ DETALLES ----------------------------------------------------------------------------------
                                #region Detalle 
                                XmlNodeList xnDetalles = xml.SelectNodes("factura/detalles/detalle");
                                foreach (XmlNode xn in xnDetalles)
                                {
                                        string codigoPrincipal = xn["codigoPrincipal"].InnerText;
                                        string descripcion = xn["descripcion"].InnerText;
                                        string cantidad = xn["cantidad"].InnerText;
                                        string precioUnitario = xn["precioUnitario"].InnerText;
                                        string descuento = xn["descuento"].InnerText;
                                        string precioTotalSinImpuesto = xn["precioTotalSinImpuesto"].InnerText;

                                        XmlNodeList xnListdetalleimpuesto = xml.SelectNodes("factura/detalles/detalle/impuestos/impuesto");

                                        foreach (XmlNode xns in xnListdetalleimpuesto)
                                        {
                                            string imcodigo = xns["codigo"].InnerText;
                                            string imcodigoPorcentaje = xns["codigoPorcentaje"].InnerText;
                                            string imtarifa = xns["tarifa"].InnerText;
                                            string imbaseImponible = xns["baseImponible"].InnerText;
                                            string imvalor = xns["valor"].InnerText;
                                        }
                                        //Termina Barrido de XML DETALLE
                                        listBox1.Items.Add("Detalle Ingresado Correctamente: ");
                            }
                            #endregion
                            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            //------------------------------------------------------------------------ Reembolsos ----------------------------------------------------------------------------------

                            #region Reembolsos 
                            XmlNodeList xnReembolsos = xml.SelectNodes("factura/reembolsos/reembolsoDetalle");
                                foreach (XmlNode xs in xnReembolsos)
                                {
                                    string tipoIdentificacionProveedorReembolso = xs["tipoIdentificacionProveedorReembolso"].InnerText;
                                    string identificacionProveedorReembolso = xs["identificacionProveedorReembolso"].InnerText;
                                    string codPaisPagoProveedorReembolso = xs["codPaisPagoProveedorReembolso"].InnerText;
                                    string tipoProveedorReembolso = xs["tipoProveedorReembolso"].InnerText;
                                    string codDocReembolso = xs["codDocReembolso"].InnerText;
                                    string estabDocReembolso = xs["estabDocReembolso"].InnerText;
                                    string ptoEmiDocReembolso = xs["ptoEmiDocReembolso"].InnerText;
                                    string secuencialDocReembolso = xs["secuencialDocReembolso"].InnerText;
                                    string fechaEmisionDocReembolso = xs["fechaEmisionDocReembolso"].InnerText;
                                    string numeroautorizacionDocReemb = xs["numeroautorizacionDocReemb"].InnerText;

                                    XmlNodeList xnListdetalleimpuestoreembolso = xml.SelectNodes("factura/reembolsos/reembolsoDetalle/detalleImpuestos/detalleImpuesto");

                                    foreach (XmlNode xns in xnListdetalleimpuestoreembolso)
                                    {
                                        string imcodigo = xns["codigo"].InnerText;
                                        string imcodigoPorcentaje = xns["codigoPorcentaje"].InnerText;
                                        string imtarifa = xns["tarifa"].InnerText;
                                        string imbaseImponibleReembolso = xns["baseImponibleReembolso"].InnerText;
                                        string imimpuestoReembolso = xns["impuestoReembolso"].InnerText;
                                    }
                                    //Termina Barrido de XML DETALLE
                                    listBox1.Items.Add("Reembolsos Ingresado Correctamente: ");

                            }
                            #endregion
                        
                                file.Close();
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Leer el archivo .xml, asegurese que es el correcto: " + ex.Message);
                    //file.WriteLine("Error al Leer el archivo .xml, asegurese que es el correcto: " + ex.Message);
                }

            }

        }

    }
}
