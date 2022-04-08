import React, { useEffect, useState } from "react";
import { Navbar, NavbarBrand, NavItem, NavLink } from "reactstrap";
import { Link } from "react-router-dom";
import "./styles/NavMenu.css";
import { useHistory } from "react-router-dom";

export function NavMenu({ setToken, token, onLayoutChange }) {
  const history = useHistory();  

  const handleClick = (e) => {
    e.preventDefault();
    setToken(undefined);
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("currentUserId");
    onLayoutChange("","center")
    onLayoutChange("groupSelection","left")
    onLayoutChange("","right")
    history.push("/");
  };

  useEffect(()=> {
    return () => {
      setToken(sessionStorage.getItem("token"));
    };
  })

  return (
    <header>
      <Navbar className="nav">
        <NavbarBrand tag={Link} to="/" className="menuLogo">
          OnePage
        </NavbarBrand>
        <ul className="menuItems">
          {token === null ? (
            <>
                <NavItem>
                  <NavLink tag={Link} to="/register">
                    Register
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} to="/login">
                    LogIn
                  </NavLink>
                </NavItem>
            </>
          ) : (
            <>
                <NavItem>
                  <NavLink tag={Link} onClick={() => onLayoutChange("users","right")}>
                    Users
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} onClick={(e) => handleClick(e)}>Log Out</NavLink>
                </NavItem>
            </>
          )}
        </ul>
      </Navbar>
    </header>
  );
}