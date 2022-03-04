import React, { useState } from "react";
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap";
import { Link } from "react-router-dom";
import "./NavMenu.css";

export function NavMenu({setToken, token}) {
  const [collapsed, setCollapsed] = useState();

  const handleClick = (e) => {
    e.preventDefault();
    setToken(null);
    sessionStorage.removeItem("token");
  } 

  return (
    <header>
      <Navbar className="nav">
        <NavbarBrand tag={Link} to="/" className="menuLogo">
          One
        </NavbarBrand>
        <ul className="menuItems">
          {token == "" || token == undefined ? (
            <>
              <li>
                <NavLink tag={Link} to="/register">
                  Register
                </NavLink>

              </NavItem>
              {token == null ? (
                <>
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/register">
                      Register
                    </NavLink>
                  </NavItem>
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/login">
                      LogIn
                    </NavLink>
                  </NavItem>
                </>
              ) : (
                <NavItem>
                  <button onClick={(e) => handleClick(e)}>Log Out</button>
                </NavItem>
              )}
            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header >
  );
};
