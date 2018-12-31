<?xml version="1.0" encoding="utf-8" ?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xhtml="http://www.w3.org/1999/xhtml" >
    <xsl:output method="xml" media-type="text/html"
        doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN" doctype-system="DTD/xhtml1-strict.dtd"/>

    <xsl:template match="/">
        <html xmlns="http://www.w3.org/1999/xhtml">
            <head>
              <title>UO Packet Guide</title>
              <style title="light">
                .toc {
                display:inline;float:right;border:solid 1px black;
                }
                .packet {
                border: 2px solid #808080; width:600px; background-color:#FFF8DC; border-collapse:collapse;
                }

                .packet-header {
                background-color: #FFD700; text-align: right;
                }

                .packet-desc {
                display: inline; float: left;
                }

                .packet-from {
                display: inline; float: right;
                }

                .packet-field-type {
                border-style: solid none solid solid; border-width:1px; border-color:#808080; width:70px;
                }

                .packet-field-desc {
                border-style: solid solid solid none; border-width: 1px; border-color: #808080; width:524px;
                }
              </style>
              <style title="dark">
                body {
                background: black;
                color: green;
                }

                .toc {
                display:inline;float:right;border:solid 1px green;
                }
                .packet {
                border: 2px solid #808080; width:600px; border-collapse:collapse;
                }

                .packet-header {
                text-align: right;
                }

                .packet-desc {
                display: inline; float: left;
                }

                .packet-from {
                display: inline; float: right;
                }

                .packet-field-type {
                border-style: solid none solid solid; border-width:1px; border-color:#808080; width:70px;
                }

                .packet-field-desc {
                border-style: solid solid solid none; border-width: 1px; border-color: #808080; width:524px;
                }
              </style>
            </head>
            <body>
                <span id="top"/>
                <h2>UO Packet Guide (Compiled by Wyatt; Design: Garret)</h2>
                <h2>WARNING: NOT FOR RUNUO.COM USERS</h2>
                <h2>Last Update: <xsl:value-of select="Packets/@lastupdate"/></h2>
                <h4>Notes: word = 2 bytes, dword = 4 bytes, qword = 8 bytes, char = 1 byte, uchar = 2 bytes, sbyte = signed byte, loop and endloop = cycle.</h4>
                <table class="toc" style="">
                    <tr>
                        <th>Table of contents</th>
                    </tr>
                    <xsl:if test="Packets/Packet[@from='client']">
                        <tr>
                            <td>Client:</td>
                        </tr>
                    </xsl:if>
                    <xsl:for-each select="Packets/Packet[@from='client']">
                        <tr>
                            <td>
                                <a href="#{concat(@from,@id)}">
                                    <xsl:value-of select="concat(@id,' - ',Name)"/>
                                </a>
                            </td>
                        </tr>
                    </xsl:for-each>
                    <xsl:if test="Packets/Packet[@from='server']">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Server:</td>
                        </tr>
                    </xsl:if>
                    <xsl:for-each select="Packets/Packet[@from='server']">
                        <tr>
                            <td>
                                <a href="#{concat(@from,@id)}">
                                    <xsl:value-of select="concat(@id,' - ',Name)"/>
                                </a>
                            </td>
                        </tr>
                    </xsl:for-each>
                    <xsl:if test="Packets/Packet[@from='both']">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Both:</td>
                        </tr>
                    </xsl:if>
                    <xsl:for-each select="Packets/Packet[@from='both']">
                        <tr>
                            <td>
                                <a href="#{concat(@from,@id)}">
                                    <xsl:value-of select="concat(@id,' - ',Name)"/>
                                </a>
                            </td>
                        </tr>
                    </xsl:for-each>
                    <xsl:if test="Packets/Packet[@from='old']">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>No longer used:</td>
                        </tr>
                    </xsl:if>
                    <xsl:for-each select="Packets/Packet[@from='old']">
                        <tr>
                            <td>
                                <a href="#{concat(@from,@id)}">
                                    <xsl:value-of select="concat(@id,' - ',Name)"/>
                                </a>
                            </td>
                        </tr>
                    </xsl:for-each>
                    <xsl:if test="Packets/Packet[@from='godclient']">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>God Client:</td>
                        </tr>
                    </xsl:if>
                    <xsl:for-each select="Packets/Packet[@from='godclient']">
                        <tr>
                            <td>
                                <a href="#{concat(@from,@id)}">
                                    <xsl:value-of select="concat(@id,' - ',Name)"/>
                                </a>
                            </td>
                        </tr>
                    </xsl:for-each>
                    <xsl:if test="Packets/Packet[@from='unknown']">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Unknown:</td>
                        </tr>
                    </xsl:if>
                    <xsl:for-each select="Packets/Packet[@from='unknown']">
                        <tr>
                            <td>
                                <a href="#{concat(@from,@id)}">
                                    <xsl:value-of select="concat(@id,' - ',Name)"/>
                                </a>
                            </td>
                        </tr>
                    </xsl:for-each>
                </table>
                <xsl:for-each select="Packets/Packet">
                    <span id="{concat(@from,@id)}"/>
                    <table class="packet" style="">
                        <tr>
                            <td colspan="2" class="packet-header" style="">
                                <xsl:value-of select="concat(@id,' - ',Name)"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="packet-desc" style="">
                                    <xsl:value-of select="Desc"/>
                                    <br />
                                    <xsl:if test="@size&gt;-1">
                                        <xsl:value-of select="concat(@size,' byte')"/>
                                        <xsl:if test="@size&gt;1">
                                            <xsl:text>s</xsl:text>
                                        </xsl:if>
                                    </xsl:if>
                                </div>
                                <div class="packet-from" style="">
                                    <xsl:value-of select="concat('from ',@from)"/>
                                </div>
                            </td>
                        </tr>
                      <xsl:for-each select="Data">
                        <tr>
                          <td class="packet-field-type" style="">
                            <xsl:value-of select="@type"/>
                            <xsl:if test="@amount">
                              <xsl:value-of select="concat('[',@amount,']')"/>
                            </xsl:if>
                          </td>
                          <td class="packet-field-desc" style="">
                            <xsl:value-of select="."/>
                          </td>
                        </tr>
                      </xsl:for-each>

                      <xsl:for-each select="Note">
                        <tr>
                          <td colspan="2">
                            <xsl:copy-of select="."/>
                          </td>
                        </tr>
                      </xsl:for-each>

                      <xsl:for-each select="Link">
                        <tr>
                          <td colspan="2">
                            <a href="{.}">
                              <xsl:choose>
                                <xsl:when test="@title">
                                  <xsl:value-of select="@title"/>
                                </xsl:when>
                                <xsl:otherwise>
                                  <xsl:value-of select="."/>
                                </xsl:otherwise>
                              </xsl:choose>
                            </a>
                          </td>
                        </tr>
                      </xsl:for-each>
                    </table>
                    <br/>
                    <a href="#top" style="float:right;">&#8593;</a>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
