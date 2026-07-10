import axios from 'axios';

export const api = axios.create({
  baseURL: 'http://localhost:5011/api', // URL do nosso Backend C# no Docker
  headers: {
    'Content-Type': 'application/json',
  },
});
