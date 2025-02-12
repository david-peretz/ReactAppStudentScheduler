import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7276/api', // עדכון לנתיב הנכון
});

export default api;