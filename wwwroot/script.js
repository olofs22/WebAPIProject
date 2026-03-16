const todoInput = document.querySelector('#todoInput');
const todo = document.querySelector('#todo');
const moodSelect = document.querySelector('#moodSelect');
const card = document.querySelector('#card');
const avatar = document.querySelector('#avatar');
const moodText = document.querySelector('#moodText');
const avatarBtn = document.querySelector('#avatarBtn')
const avatars = ['🐱', '🐶', '🦊', '🐸', '🦄', '🐼', '🦁', '🐯'];
const filterSelect = document.querySelector('#filterSelect');

todoInput.addEventListener('input', function (event) {
    const typedTodo = event.target.value;
    todo.textContent = typedTodo; 
    todo.textContent = typedTodo; 
})

const addBtn = document.querySelector('#addBtn');
const todoList = document.querySelector('#todoList');
const stats = document.querySelector('#stats');

let todos = []; 


async function loadTodos() {
    const response = await fetch('/api/todo');
    todos = await response.json();
    render();
}

async function addTodo() {
    const text = todoInput.value.trim();
    if (text === '') return;

    const response = await fetch('/api/todo', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ title: text, done: false })
    });
    const newTodo = await response.json();
    todos.push(newTodo);

    todoInput.value = '';
    render();
}

async function toggleTodo(id, done) {
    const todo = todos.find(t => t.id === id);
    await fetch(`/api/todo/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: todo.id, title: todo.title, done: done })
    });
    todo.done = done;
    render();
}

async function deleteTodo(id) {
    await fetch(`/api/todo/${id}`, { method: 'DELETE' });
    todos = todos.filter(t => t.id !== id);
    render();
}

addBtn.addEventListener('click', addTodo);

todoInput.addEventListener('keydown', function (event) {
    if (event.key === 'Enter') addTodo();
})

todoList.addEventListener('click', function (event) {
    const target = event.target;
    const li = target.closest('li');
    if (!li) return;
    const id = Number(li.dataset.id);

    if (target.type === 'checkbox') {
        toggleTodo(id, target.checked);
    }
    if (target.dataset.action === 'delete') {
        deleteTodo(id);
    }
});

filterSelect.addEventListener('change', render);
function render() {
    const filter = filterSelect.value;

    const visible = todos.filter(function (todo) {
        if (filter === 'active') return todo.done === false;
        if (filter === 'done') return todo.done === true;
        return true;
    });
    todoList.textContent = '';

    visible.forEach(function (todo) {
        const li = document.createElement('li');
        li.dataset.id = todo.id;

        const checkbox = document.createElement('input');
        checkbox.type = 'checkbox';
        checkbox.checked = todo.done;

        const span = document.createElement('span');
        span.textContent = todo.title;

        const deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.dataset.action = 'delete';

        li.appendChild(checkbox);
        li.appendChild(span);
        li.appendChild(deleteBtn);

        todoList.appendChild(li);
    });

    updateStats();
}

function updateStats() {
    const total = todos.length;
    const done = todos.filter(t => t.done).length;
    stats.textContent = 'Total: ' + total + '| Done' + done;
}

loadTodos();