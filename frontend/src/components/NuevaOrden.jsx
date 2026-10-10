import { useState } from 'react'

const MinimoProductos = 2
const itemVacio = { productoId: '', cantidad: 1 }
const itemsIniciales = Array.from({ length: MinimoProductos }, () => ({ ...itemVacio }))

export default function NuevaOrden({ clientes, productos, onCrear }) {
  const [clienteId, setClienteId] = useState('')
  const [items, setItems] = useState(itemsIniciales)

  const cambiarItem = (indice, campo, valor) =>
    setItems(items.map((item, posicion) => (posicion === indice ? { ...item, [campo]: valor } : item)))

  const quitarItem = (indice) => setItems(items.filter((_, posicion) => posicion !== indice))

  const crear = async (evento) => {
    evento.preventDefault()
    const orden = await onCrear({
      clienteId: Number(clienteId),
      items: items.map((item) => ({
        productoId: Number(item.productoId),
        cantidad: Number(item.cantidad),
      })),
    })

    if (orden) {
      setClienteId('')
      setItems(itemsIniciales)
    }
  }

  return (
    <>
      <h2>Nueva orden</h2>
      <form onSubmit={crear} className="columna">
        <select value={clienteId} onChange={(evento) => setClienteId(evento.target.value)} required>
          <option value="">Seleccione un cliente</option>
          {clientes.map((cliente) => (
            <option key={cliente.id} value={cliente.id}>
              {cliente.nombre} {cliente.apellido}
            </option>
          ))}
        </select>

        {items.map((item, indice) => (
          <div key={indice} className="fila">
            <select
              value={item.productoId}
              onChange={(evento) => cambiarItem(indice, 'productoId', evento.target.value)}
              required
            >
              <option value="">Seleccione un producto</option>
              {productos.map((producto) => (
                <option key={producto.id} value={producto.id}>
                  {producto.nombre} (stock: {producto.stock})
                </option>
              ))}
            </select>
            <input
              type="number"
              min="1"
              value={item.cantidad}
              onChange={(evento) => cambiarItem(indice, 'cantidad', evento.target.value)}
              required
            />
            {items.length > MinimoProductos && (
              <button type="button" className="peligro" onClick={() => quitarItem(indice)}>
                Quitar
              </button>
            )}
          </div>
        ))}

        <div className="fila">
          <button type="button" onClick={() => setItems([...items, { ...itemVacio }])}>
            Agregar producto
          </button>
          <button type="submit">Crear orden</button>
        </div>
      </form>
    </>
  )
}
