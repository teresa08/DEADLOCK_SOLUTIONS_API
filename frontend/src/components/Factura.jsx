import { formatearMoneda } from '../api.js'

export default function Factura({ factura, onCerrar }) {
  return (
    <article className="factura">
      <header>
        <strong>Factura {factura.numero}</strong>
        <button onClick={onCerrar}>Cerrar</button>
      </header>
      <p>
        Fecha: {new Date(factura.fecha).toLocaleDateString('es')} | Orden #{factura.ordenId}
      </p>
      <p>
        Cliente: {factura.cliente.nombre} {factura.cliente.apellido}
      </p>
      <table>
        <thead>
          <tr>
            <th>Producto</th>
            <th>Cantidad</th>
            <th>Precio</th>
            <th>Subtotal</th>
          </tr>
        </thead>
        <tbody>
          {factura.items.map((item) => (
            <tr key={item.id}>
              <td>{item.producto}</td>
              <td>{item.cantidad}</td>
              <td>{formatearMoneda(item.precioUnitario)}</td>
              <td>{formatearMoneda(item.subtotal)}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <p className="total">Total: {formatearMoneda(factura.total)}</p>
    </article>
  )
}
