using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MVC_WebSite.Models.SIE
{
    public class ListaValores
    {
        public List<DataSerie> data { get; set; }
    }

    [DataContract]
    public class Serie
    {
        [DataMember(Name = "titulo",Order =1)]
        public string Title { get; set; }

        [Display(Name ="Id Serie", Order = 2)]
        [DataMember(Name = "idSerie")]
        public string IdSerie { get; set; }

        [Display(Name = "Cifra", Order = 3)]
        [DataMember(Name = "cifra")]
        public string Cifra { get; set; }

        [Display(Name = "Periodicidad",Order = 4)]
        [DataMember(Name = "periodicidad")]
        public string Periodicidad { get; set; }

        [Display(Name = "Fecha Inicio",Order = 5)]
        [DataMember(Name = "fechaInicio")]
        public string FechaInicio { get; set; }

        [Display(Name = "Fecha Fin", Order = 6)]
        [DataMember(Name = "fechaFin")]
        public string FechaFin { get; set; }
        
        [DataMember(Name = "datos")]
        public DataSerie[] Data { get; set; }

    }

    [DataContract]
    public class DataSerie
    {
        [DataMember(Name = "fecha")]
        public string Date { get; set; }

        [DataMember(Name = "dato")]
        public string Data { get; set; }
    }

    [DataContract]
    class SeriesResponse
    {
        [DataMember(Name = "series")]
        public Serie[] series { get; set; }

    }

    [DataContract]
    class Response
    {
        [DataMember(Name = "bmx")]
        public SeriesResponse seriesResponse { get; set; }
    }
}