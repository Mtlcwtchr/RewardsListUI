I implemented the Model-View-Presenter (MVP) pattern and used JSON to represent the rewards data.
This approach was selected for its alignment with the principles of modularity, scalability, and maintainability.

JSON is easily readable and interpretable by both developers and non-developers, making debugging and validation straightforward.  
It is also a standard format for backend requests and is compatible with a wide range of tools, libraries, and programming environments.
JSON arrays allow for storing multiple rewards in a structured format. Each reward is represented as an object containing properties like title, message, avatar (URL), and reward amount, providing scalability for additional fields as needed.
JSON data can be fetched either locally or remotely, ensuring flexibility in implementation and testing.

MVP advantages are:
 Presenters common logic (e.g., data fetching and validation) can be reused across multiple Views.
 Components are modular: Each component (Model, Presenter) is self-contained and can be replaced or extended independently.
 Since the logic resides in the Presenter, unit testing becomes straightforward. Mock Models and Views can be used to verify the behavior of the Presenter.
 Adding new features in UI can be done by extending the Presenter and updating the View without modifying the underlying Model. Model extending also would not affect View if updates are not related to both view and model.

Small UI optimisation: place mostly static and mostly dynamic objects on two different canvases. 
Instead of UIController and repository update from reward element model it could be better to use some kind of signals or global events with some event router.
Injector is some kind of simplified service binding root while using Zenject while I have no third-party DI imported.
