import { useState } from 'react';

export default function useToken() {
  const getToken = () => {
    const tokenString = sessionStorage.getItem('token');

    if (
      tokenString === null ||
      tokenString.includes("null") ||
      tokenString === undefined
    ) {
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
