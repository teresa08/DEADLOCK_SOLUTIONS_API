import { useEffect, useState } from 'react'
import { api, formatearMoneda } from '../api.js'

const productoVacio = { nombre: '', precio: '', stock: '' }

export default function Productos({ notificar }) {
  const [productos, setProductos] = useState([])
  const [producto, setProducto] = useState(productoVacio)

  const cargar = () =>
    api.listarProductos().then(setProductos).catch((error) => notificar(error.message, true))

  useEffect(() => {
    cargar()
  }, [])

  const cambiar = (evento) => setProducto({ ...producto, [evento.target.name]: evento.target.value })

  const registrar = async (evento) => {
    evento.preventDefault()
    try {
      await api.registrarProducto({
        nombre: producto.nombre,
        precio: Number(producto.precio),
        stock: Number(producto.stock),
      })
      setProducto(productoVacio)
      notificar('Producto registrado.')
      cargar()
    } catch (error) {
      notificar(error.message, true)
    }
  }

  return (
    <section>
      <h2>Registrar producto</h2>
      <form onSubmit={registrar}>
        <input name="nombre" placeholder="Nombre" value={producto.nombre} onChange={cambiar} required />
        <input name="precio" type="number" min="0.01" step="0.01" placeholder="Precio" value={producto.precio} onChange={cambiar} required />
        <input name="stock" type="number" min="0" placeholder="Stock" value={producto.stock} onChange={cambiar} required />
        <button type="submit">Registrar</button>
      </form>

      <h2>Productos</h2>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Nombre</th>
            <th>Precio</th>
            <th>Stock</th>
          </tr>
        </thead>
        <tbody>
          {productos.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.nombre}</td>
              <td>{formatearMoneda(item.precio)}</td>
              <td>{item.stock}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}
