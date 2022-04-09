import React, { useEffect } from "react";
import { Navbar, NavbarBrand, NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import "./styles/NavMenu.css";
import { useHistory } from "react-router-dom";

export function NavMenu({ setToken, onLayoutChange }) {
  const history = useHistory();

  const handleClick = (e) => {
    e.preventDefault();
    setToken(undefined);
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("currentUserId");
    onLayoutChange("", "center");
    onLayoutChange("groupSelection", "left");
    onLayoutChange("", "right");
    history.push("/");
  };

  useEffect(() => {
    return () => {
      setToken(sessionStorage.getItem("token"));
    };
  });

  return (
    <header>
      <Navbar className="nav">
        <button className='logo-btn' onClick={() => onLayoutChange('timeline','center')}>
          <img className='logo-btn-img' src = '/resources/Logo.png' alt='Logo'/>
        </button>
        <ul className="menuItems">
          <>
            <NavItem>
              <NavLink
                to="/"
                tag={Link}
                onClick={() => onLayoutChange("users", "right")}
              >
                Users
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink
                to="/"
                tag={Link}
                onClick={() => onLayoutChange("settings-about", "center")}
              >
                Settings
              </NavLink>
            </NavItem>
            <NavItem>
              <NavLink to="/" tag={Link} onClick={(e) => handleClick(e)}>
                Log Out
              </NavLink>
            </NavItem>
          </>
        </ul>
      </Navbar>
    </header>
  );
}
