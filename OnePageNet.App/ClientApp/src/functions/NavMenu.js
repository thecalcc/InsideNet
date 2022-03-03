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

export const NavMenu = ({ setToken, token }) => {
  const [collapsed, setCollapsed] = useState();

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        light
      >
        <Container>
          <NavbarBrand tag={Link} to="/">
            OnePageNet.App
          </NavbarBrand>
          <NavbarToggler
            onClick={(e) => setCollapsed(e.target.value)}
            className="mr-2"
          />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={!collapsed}
            navbar
          >
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">
                  Home
                </NavLink>
              </NavItem>
              {token == "" || token == undefined ? (
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
                  <button onClick={setToken("")}>Log Out</button>
                </NavItem>
              )}
            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  );
};