const request = async (path, method = 'GET', body) => {
  const response = await fetch(`/api/${path}`, {
    method,
    headers: body ? { 'Content-Type': 'application/json' } : undefined,
    body: body ? JSON.stringify(body) : undefined,
  })
  const result = await response.json()

  if (!result.isSuccess) {
    throw new Error(result.message)
  }

  return result.data
}

export const api = {
  listarClientes: () => request('clientes'),
  registrarCliente: (cliente) => request('clientes', 'POST', cliente),
  listarProductos: () => request('productos'),
  registrarProducto: (producto) => request('productos', 'POST', producto),
  listarOrdenes: () => request('ordenes'),
  crearOrden: (orden) => request('ordenes', 'POST', orden),
  actualizarCantidad: (ordenId, itemId, cantidad) =>
    request(`ordenes/${ordenId}/items/${itemId}`, 'PUT', { cantidad }),
  eliminarItem: (ordenId, itemId) => request(`ordenes/${ordenId}/items/${itemId}`, 'DELETE'),
  generarFactura: (ordenId) => request(`ordenes/${ordenId}/factura`, 'POST'),
  obtenerFactura: (ordenId) => request(`ordenes/${ordenId}/factura`),
}

export const formatearMoneda = (valor) =>
  valor.toLocaleString('es', { style: 'currency', currency: 'USD' })
