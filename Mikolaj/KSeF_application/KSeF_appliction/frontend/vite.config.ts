import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        target: process.env.SERVER_HTTPS || process.env.SERVER_HTTP,
        changeOrigin: true
      }
    }
  }
})
