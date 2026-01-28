package com.bookmyturf.entity;

import jakarta.persistence.*;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;

import java.time.LocalDateTime;

@Entity
@Table(name = "SPORTS")
public class Sports {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "SportID")
    private Integer sportId;

    @Column(name = "SportName", nullable = false, unique = true)
    private String sportName;

    @Column(name = "DefaultRules")
    private String defaultRules;

    @Column(name = "IsActive")
    private Boolean isActive = true;

    @CreationTimestamp
    @Column(name = "CreatedDate", updatable = false)
    private LocalDateTime createdDate;

    @UpdateTimestamp
    @Column(name = "UpdatedDate")
    private LocalDateTime updatedDate;

    // Getters and Setters
    public Integer getSportId() { return sportId; }
    public void setSportId(Integer sportId) { this.sportId = sportId; }

    public String getSportName() { return sportName; }
    public void setSportName(String sportName) { this.sportName = sportName; }

    public String getDefaultRules() { return defaultRules; }
    public void setDefaultRules(String defaultRules) { this.defaultRules = defaultRules; }

    public Boolean getIsActive() { return isActive; }
    public void setIsActive(Boolean isActive) { this.isActive = isActive; }

    public LocalDateTime getCreatedDate() { return createdDate; }
    public LocalDateTime getUpdatedDate() { return updatedDate; }
}
