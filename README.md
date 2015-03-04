# DataGrainEf
A simple way of managing data seeding for Entity Framework 6 Apps

## Introduction
A simple way of adding data migrations to migrations within Entity Framework 6.

## How To
1) Add DataGrain.AddTableToModel(modelBuilder); to your OnmodelCreating method of your DbContext. This is so the __DataMigrations table can be configured and added to the context.
2) Add DataGrain<TContext>.Seed(context, this); to your configuration seed method
3) Add as many data migration classes as you need (As long as they have the DataMigration attribute and inherit the IDataMigration<TContext> interface, they will be found and used appropriatly.

## TODO
 - Complete Instructions
 - Automate Testing
