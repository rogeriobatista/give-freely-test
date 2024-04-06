import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { logout } from 'store/login';

const SignOutPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  
  useEffect(() => {
    dispatch(logout);
    navigate("/sign-in");
  }, []);

  return <></>
}

export default SignOutPage