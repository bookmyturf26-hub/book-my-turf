package com.bookmyturf.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "SPORTS")
public class Sports {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer sportID;

    private String sportName;
    private String defaultRules;
    private Boolean isActive;

    public Sports() {}

    public Sports(Integer sportID, String sportName, String defaultRules, Boolean isActive) {
        this.sportID = sportID;
        this.sportName = sportName;
        this.defaultRules = defaultRules;
        this.isActive = isActive;
    }

    public Integer getSportID() { return sportID; }
    public void setSportID(Integer sportID) { this.sportID = sportID; }

    public String getSportName() { return sportName; }
    public void setSportName(String sportName) { this.sportName = sportName; }

    public String getDefaultRules() { return defaultRules; }
    public void setDefaultRules(String defaultRules) { this.defaultRules = defaultRules; }

    public Boolean getIsActive() { return isActive; }
    public void setIsActive(Boolean isActive) { this.isActive = isActive; }
}
