import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_BASE_URL || 'https://localhost:7126',
  headers: {
    'Content-Type': 'application/json'//,
    // common: {
    //   'Authorization': `Bearer ${localStorage.getItem('token') || ''}`,
    // }
  },
});

export default axiosInstance;
