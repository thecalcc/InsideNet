import { useState } from 'react';

export default function useToken() {
  const getToken = () => {
    const tokenString = sessionStorage.getItem('token');

    if (tokenString.includes("null") || tokenString === null) {
      return null;
    }
    return tokenString;
  };
  
  const [token, setToken] = useState(() => getToken());

  return {
    setToken,
    token,
  };
}
