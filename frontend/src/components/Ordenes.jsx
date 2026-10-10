import { useEffect, useState } from 'react'
import { api, formatearMoneda } from '../api.js'
import NuevaOrden from './NuevaOrden.jsx'
import Factura from './Factura.jsx'

export default function Ordenes({ notificar }) {
  const [ordenes, setOrdenes] = useState([])
  const [clientes, setClientes] = useState([])
  const [productos, setProductos] = useState([])
  const [factura, setFactura] = useState(null)

  const cargar = async () => {
    try {
      const [listaOrdenes, listaClientes, listaProductos] = await Promise.all([
        api.listarOrdenes(),
        api.listarClientes(),
        api.listarProductos(),
      ])
      setOrdenes(listaOrdenes)
      setClientes(listaClientes)
      setProductos(listaProductos)
    } catch (error) {
      notificar(error.message, true)
    }
  }

  useEffect(() => {
    cargar()
  }, [])

  const ejecutar = async (accion, mensaje) => {
    try {
      const resultado = await accion()
      notificar(mensaje)
      await cargar()
      return resultado
    } catch (error) {
      notificar(error.message, true)
    }
  }

  const actualizarCantidad = (ordenId, item) => {
    const cantidad = Number(window.prompt(`Nueva cantidad para ${item.producto}`, item.cantidad))

    if (cantidad > 0) {
      ejecutar(() => api.actualizarCantidad(ordenId, item.id, cantidad), 'Cantidad actualizada.')
    }
  }

  const generarFactura = async (ordenId) =>
    setFactura(await ejecutar(() => api.generarFactura(ordenId), 'Factura generada.'))

  const verFactura = (ordenId) =>
    api.obtenerFactura(ordenId).then(setFactura).catch((error) => notificar(error.message, true))

  return (
    <section>
      <NuevaOrden
        clientes={clientes}
        productos={productos}
        onCrear={(orden) => ejecutar(() => api.crearOrden(orden), 'Orden creada.')}
      />

      {factura && <Factura factura={factura} onCerrar={() => setFactura(null)} />}

      <h2>Ordenes</h2>
      {ordenes.map((orden) => (
        <article key={orden.id}>
          <header>
            <strong>
              Orden #{orden.id} - {orden.cliente.nombre} {orden.cliente.apellido}
            </strong>
            {orden.facturada ? (
              <button onClick={() => verFactura(orden.id)}>Ver factura</button>
            ) : (
              <button onClick={() => generarFactura(orden.id)}>Generar factura</button>
            )}
          </header>
          <table>
            <thead>
              <tr>
                <th>Producto</th>
                <th>Cantidad</th>
                <th>Precio</th>
                <th>Subtotal</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {orden.items.map((item) => (
                <tr key={item.id}>
                  <td>{item.producto}</td>
                  <td>{item.cantidad}</td>
                  <td>{formatearMoneda(item.precioUnitario)}</td>
                  <td>{formatearMoneda(item.subtotal)}</td>
                  <td>
                    {!orden.facturada && (
                      <>
                        <button onClick={() => actualizarCantidad(orden.id, item)}>Editar</button>
                        <button
                          className="peligro"
                          onClick={() => ejecutar(() => api.eliminarItem(orden.id, item.id), 'Item eliminado.')}
                        >
                          Eliminar
                        </button>
                      </>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <p className="total">Total: {formatearMoneda(orden.total)}</p>
        </article>
      ))}
    </section>
  )
}
