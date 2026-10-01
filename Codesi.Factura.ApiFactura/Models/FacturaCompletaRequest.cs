using System;
using System.Collections.Generic;

namespace Codesi.Factura.Api.Models
{
    public class FacturaCompletaRequest
    {
        public string establecimiento { get; set; } = "001";

        public string punto_emision { get; set; } = "001";

        public DateTime fecha_emision { get; set; } = DateTime.Now;

        public int id_cliente { get; set; }

        public decimal subtotal_sin_impuestos { get; set; }

        public decimal total_descuento { get; set; }

        public decimal subtotal_iva { get; set; }

        public decimal propina { get; set; }

        public decimal importe_total { get; set; }

        public string estado_sri { get; set; } = "CREADA";

        public List<FacturaDetalleRequest> detalles { get; set; } = new();

        public string forma_pago { get; set; } = string.Empty;

        public decimal total_pago { get; set; }
    }
}