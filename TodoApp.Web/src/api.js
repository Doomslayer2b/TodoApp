const API_BASE = 'http://localhost:7283';

export async function getTodos() {
  const res = await fetch(`${API_BASE}/todoitems`);
  if (!res.ok) throw new Error('Failed to fetch todos');
  return res.json();
}

export async function createTodo(todo) {
  const res = await fetch(`${API_BASE}/todoitems`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(todo),
  });
  if (!res.ok) throw new Error('Failed to create todo');
  return res.json();
}

export async function deleteTodo(id) {
  await fetch(`${API_BASE}/todoitems/${id}`, {
    method: 'DELETE',
  });
}
