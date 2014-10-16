using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using System.Xml.Linq;

namespace Appcatlan
{
    public partial class PivotPage : PhoneApplicationPage
    {
        private string datos;

        public PivotPage()
        {
            InitializeComponent();
            getData();
        }

        private void getData()
        {
            try
            {

                WebClient _servidor = new WebClient();
                _servidor.DownloadStringCompleted += new DownloadStringCompletedEventHandler(_servidor_DescargarString);
                _servidor.DownloadStringAsync(new Uri("http://www.acatlan.unam.mx/rss/"));
            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void _servidor_DescargarString(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null || e.Cancelled)
                {
                    //MessageBox.Show("No hay conexión a internet");
                    Debug.WriteLine(e.Error.Message);
                    return;
                }

                datos = e.Result;
                //file.guardarXml(datos, "places.xml");
                parser(datos);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private void parser(string data)
        {
            XDocument xmlCategories = XDocument.Parse(data);

            Noticias.ItemsSource = from lista in xmlCategories.Descendants("item")
                                     select new Item
                                     {
                                         Title = lista.Element("title").Value,
                                     };
        }

    }
}