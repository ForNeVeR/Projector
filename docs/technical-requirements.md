Technical Requirements
======================

Projector is a web application for project management.

System Architecture
-------------------

Projector uses completely decoupled backend and frontend. Backend provides a web
API for frontend, and there could potentially be other frontend implementations.

User Roles
----------

There shoule be two user roles:

- Ordinary user
- System administrator

Ordinary users could create their own projects and invite other users to the
projects.

System administrator could also add, block, and reset passwords for users.

User Management
---------------

System administrator could load a user table through a web interface, and lock,
edit and create a new user from there. Administrator also could change a
password for any user.

Project Management
------------------

After logging into a system, each user could add a project and invite other
users to a project (using their names). The project creator is an only user who
could invite others to the project.

*Project* consists of *tasks* and *executors*.

Task Management
---------------

Each *task* has the following characteristics:

- Name
- Depends on (item list)
- Duration (time)
- Start no earlier than (date + time)
- Current start date + time
- Executor assigned

Executor Management
-------------------

*Executor* is a personnel who will execute the tasks.

Each *executor* can work on one task at a time.

Task scheduler
--------------

Main Projector screen should be a task scheduler which should consist of a Gantt
diagram combined with a task table. From there, user could change task start
dates and and of task attributes.

Projector shouldn't prevent the user from business rules violation (scheduling
the *task* before its depencencies or making an *executor* to perform two tasks
at once), but should show user a warning.
