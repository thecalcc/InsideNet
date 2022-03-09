import { useState } from 'react';

export default function useToken() {
  const getToken = () => {
    const tokenString = sessionStorage.getItem('token');
    const userToken = JSON.stringify(tokenString);
    if(userToken.includes('null') || userToken === null){
      return null;
    }
    return userToken?.token
  };
  
  const [token, setToken] = useState(getToken());

  return {
    setToken,
    token,
  };
}
