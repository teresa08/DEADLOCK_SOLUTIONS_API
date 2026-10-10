import { useEffect, useState } from 'react'
import { api } from '../api.js'

const clienteVacio = { nombre: '', apellido: '', telefono: '', correo: '', direccion: '' }

export default function Clientes({ notificar }) {
  const [clientes, setClientes] = useState([])
  const [cliente, setCliente] = useState(clienteVacio)

  const cargar = () =>
    api.listarClientes().then(setClientes).catch((error) => notificar(error.message, true))

  useEffect(() => {
    cargar()
  }, [])

  const cambiar = (evento) => setCliente({ ...cliente, [evento.target.name]: evento.target.value })

  const registrar = async (evento) => {
    evento.preventDefault()
    try {
      await api.registrarCliente({ ...cliente, correo: cliente.correo || null })
      setCliente(clienteVacio)
      notificar('Cliente registrado.')
      cargar()
    } catch (error) {
      notificar(error.message, true)
    }
  }

  return (
    <section>
      <h2>Registrar cliente</h2>
      <form onSubmit={registrar}>
        <input name="nombre" placeholder="Nombre" value={cliente.nombre} onChange={cambiar} required />
        <input name="apellido" placeholder="Apellido" value={cliente.apellido} onChange={cambiar} required />
        <input name="telefono" placeholder="Telefono" value={cliente.telefono} onChange={cambiar} />
        <input name="correo" type="email" placeholder="Correo" value={cliente.correo} onChange={cambiar} />
        <input name="direccion" placeholder="Direccion" value={cliente.direccion} onChange={cambiar} />
        <button type="submit">Registrar</button>
      </form>

      <h2>Clientes</h2>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Nombre</th>
            <th>Telefono</th>
            <th>Correo</th>
            <th>Direccion</th>
          </tr>
        </thead>
        <tbody>
          {clientes.map((item) => (
            <tr key={item.id}>
              <td>{item.id}</td>
              <td>{item.nombre} {item.apellido}</td>
              <td>{item.telefono}</td>
              <td>{item.correo}</td>
              <td>{item.direccion}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </section>
  )
}
