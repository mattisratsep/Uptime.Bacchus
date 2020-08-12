import axios from 'axios';

var axiosInstance = axios.create({
    timeout: 120 * 1000,
    headers: {
        'Cache-Control': 'no-cache',
        'Pragma': 'no-cache',
        'Content-Type': 'application/json'
    },
    baseURL: 'http://localhost:5000',
    /* other custom settings */
});

export default axiosInstance;