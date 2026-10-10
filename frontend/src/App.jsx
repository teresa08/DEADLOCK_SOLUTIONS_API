import { useState } from 'react'
import Clientes from './components/Clientes.jsx'
import Productos from './components/Productos.jsx'
import Ordenes from './components/Ordenes.jsx'

const secciones = {
  Clientes,
  Productos,
  Ordenes,
}

export default function App() {
  const [seccion, setSeccion] = useState('Clientes')
  const [mensaje, setMensaje] = useState(null)
  const Seccion = secciones[seccion]

  const notificar = (texto, esError = false) => setMensaje({ texto, esError })

  const cambiarSeccion = (nombre) => {
    setMensaje(null)
    setSeccion(nombre)
  }

  return (
    <main>
      <h1>Deadlock Solutions</h1>
      <nav>
        {Object.keys(secciones).map((nombre) => (
          <button
            key={nombre}
            className={nombre === seccion ? 'activo' : ''}
            onClick={() => cambiarSeccion(nombre)}
          >
            {nombre}
          </button>
        ))}
      </nav>
      {mensaje && <p className={mensaje.esError ? 'error' : 'exito'}>{mensaje.texto}</p>}
      <Seccion notificar={notificar} />
    </main>
  )
}
