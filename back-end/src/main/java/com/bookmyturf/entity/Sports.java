package com.bookmyturf.entity;

import jakarta.persistence.*;
import java.util.Set;

@Entity
@Table(name = "SPORTS")
public class Sports {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "SportID")
    private Integer sportId;

    @Column(name = "SportName", nullable = false)
    private String sportName;

    @Column(name = "DefaultRules", columnDefinition = "TEXT")
    private String defaultRules;

    @Column(name = "IsActive")
    private Boolean isActive;

    // --- REMOVE OR COMMENT OUT THESE TWO IF THEY ARE NOT IN THE DB ---
    // @Column(name = "CreatedDate")
    // private LocalDateTime createdDate;

    // @Column(name = "UpdatedDate")
    // private LocalDateTime updatedDate;
    // ----------------------------------------------------------------

    // Getters and Setters
    public Integer getSportId() { return sportId; }
    public void setSportId(Integer sportId) { this.sportId = sportId; }

    public String getSportName() { return sportName; }
    public void setSportName(String sportName) { this.sportName = sportName; }

    public String getDefaultRules() { return defaultRules; }
    public void setDefaultRules(String defaultRules) { this.defaultRules = defaultRules; }

    public Boolean getIsActive() { return isActive; }
    public void setIsActive(Boolean isActive) { this.isActive = isActive; }
}