package com.bookmyturf.repository;

import com.bookmyturf.entity.UserType;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserTypeRepository extends JpaRepository<UserType, Integer> {
    // Find UserType by TypeName (Admin, Player, TurfOwner)
    UserType findByTypeName(String typeName);
}
