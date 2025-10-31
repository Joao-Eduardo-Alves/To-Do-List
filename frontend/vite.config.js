import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
    plugins: [
        react(),
        tailwindcss(),
    ],
    server: {
        proxy: {
            '/adicionarTarefa': 'http://localhost:5175',
            '/listarTarefas': 'http://localhost:5175',
            '/removerTarefa': 'http://localhost:5175',
            '/concluirTarefa/': 'http://localhost:5175',
            '/editarTarefa/': 'http://localhost:5175'
        }
    }
});
