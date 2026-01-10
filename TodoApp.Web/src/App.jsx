import { useEffect, useState } from "react";

function App() {
  const [todos, setTodos] = useState([]);
  const [name, setName] = useState("");
  const [categoryId, setCategoryId] = useState(1);

  const API_URL = "https://localhost:7283/todoitems";

  // GET all todos
  useEffect(() => {
    fetch(API_URL)
      .then(res => res.json())
      .then(data => setTodos(data))
      .catch(err => console.error("Fetch error:", err));
  }, []);

  // CREATE todo
  const addTodo = async () => {
    if (!name.trim()) return;

    const response = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        name,
        isComplete: false,
        categoryId
      })
    });

    const newTodo = await response.json();
    setTodos([...todos, newTodo]);
    setName("");
  };

  // DELETE todo
  const deleteTodo = async (id) => {
    await fetch(`${API_URL}/${id}`, {
      method: "DELETE"
    });
    setTodos(todos.filter(t => t.id !== id));
  };

  // UPDATE todo (toggle complete/incomplete)
  const toggleComplete = async (todo) => {
    await fetch(`${API_URL}/${todo.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        name: todo.name,
        isComplete: !todo.isComplete,
        categoryId: todo.categoryId
      })
    });

    setTodos(todos.map(t => 
      t.id === todo.id 
        ? { ...t, isComplete: !t.isComplete } 
        : t
    ));
  };

  // Handle Enter key to add todo
  const handleKeyPress = (e) => {
    if (e.key === 'Enter') {
      addTodo();
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 py-8 px-4">
      <div className="max-w-2xl mx-auto">
        {/* Header */}
        <div className="bg-white rounded-lg shadow-lg p-8 mb-6">
          <h1 className="text-4xl font-bold text-gray-800 mb-2">
            📝 To-do App
          </h1>
          <p className="text-gray-600">Stay organized, get things done!</p>
        </div>

        {/* Add Todo Section */}
        <div className="bg-white rounded-lg shadow-lg p-6 mb-6">
          <div className="flex gap-3">
            <input
              value={name}
              onChange={e => setName(e.target.value)}
              onKeyPress={handleKeyPress}
              placeholder="What needs to be done?"
              className="flex-1 px-4 py-3 border-2 border-gray-300 rounded-lg focus:outline-none focus:border-indigo-500 transition-colors"
            />
            <button 
              onClick={addTodo}
              className="px-6 py-3 bg-indigo-600 text-white font-semibold rounded-lg hover:bg-indigo-700 transition-colors shadow-md hover:shadow-lg"
            >
              Add Task
            </button>
          </div>
        </div>

        {/* Stats */}
        <div className="grid grid-cols-3 gap-4 mb-6">
          <div className="bg-white rounded-lg shadow p-4 text-center">
            <p className="text-2xl font-bold text-indigo-600">{todos.length}</p>
            <p className="text-gray-600 text-sm">Total</p>
          </div>
          <div className="bg-white rounded-lg shadow p-4 text-center">
            <p className="text-2xl font-bold text-green-600">
              {todos.filter(t => t.isComplete).length}
            </p>
            <p className="text-gray-600 text-sm">Completed</p>
          </div>
          <div className="bg-white rounded-lg shadow p-4 text-center">
            <p className="text-2xl font-bold text-orange-600">
              {todos.filter(t => !t.isComplete).length}
            </p>
            <p className="text-gray-600 text-sm">Pending</p>
          </div>
        </div>

        {/* Todos List */}
        <div className="space-y-3">
          {todos.length === 0 ? (
            <div className="bg-white rounded-lg shadow p-8 text-center">
              <p className="text-gray-400 text-lg">No todos yet. Add one to get started! 🚀</p>
            </div>
          ) : (
            todos.map(t => (
              <div 
                key={t.id} 
                className={`bg-white rounded-lg shadow-md p-5 transition-all hover:shadow-lg ${
                  t.isComplete ? 'opacity-75' : ''
                }`}
              >
                <div className="flex items-center justify-between">
                  <div className="flex items-center gap-3 flex-1">
                    <input
                      type="checkbox"
                      checked={t.isComplete}
                      onChange={() => toggleComplete(t)}
                      className="w-5 h-5 text-indigo-600 rounded cursor-pointer"
                    />
                    <span className={`text-lg ${
                      t.isComplete 
                        ? 'line-through text-gray-400' 
                        : 'text-gray-800 font-medium'
                    }`}>
                      {t.name}
                    </span>
                  </div>

                  <div className="flex gap-2">
                    <button 
                      onClick={() => toggleComplete(t)}
                      className={`px-4 py-2 rounded-lg font-medium transition-colors ${
                        t.isComplete
                          ? 'bg-yellow-100 text-yellow-700 hover:bg-yellow-200'
                          : 'bg-green-100 text-green-700 hover:bg-green-200'
                      }`}
                    >
                      {t.isComplete ? '↩️ Undo' : '✅ Done'}
                    </button>
                    
                    <button 
                      onClick={() => deleteTodo(t.id)}
                      className="px-4 py-2 bg-red-100 text-red-700 rounded-lg font-medium hover:bg-red-200 transition-colors"
                    >
                      🗑️ Delete
                    </button>
                  </div>
                </div>
              </div>
            ))
          )}
        </div>

        {/* Footer */}
        <div className="mt-8 text-center text-gray-600 text-sm">
          <p>Built with ❤️ by Andres Nieves</p>
        </div>
      </div>
    </div>
  );
}

export default App;