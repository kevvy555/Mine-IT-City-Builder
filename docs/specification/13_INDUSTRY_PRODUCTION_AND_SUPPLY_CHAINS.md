# 13 — Industry, Production and Supply Chains

## Purpose

Industry gives physical meaning to freight, employment, land use, utilities and the wider MineIT material canon.

KCB-IND-001 — Industrial simulation SHOULD reference canonical substance/material categories where relevant instead of inventing a parallel raw-material taxonomy.

KCB-IND-002 — City-builder production chains are game abstractions layered over Universe substance IDs.

## Production unit

A production facility defines:
- inputs;
- output;
- recipe/rate;
- workforce;
- power/water/other utilities;
- floor/land requirement;
- machinery/automation level;
- inventory;
- waste/by-products;
- freight interface;
- environmental externalities;
- maintenance.

effectiveOutput = nominalRate x staffingFactor x utilityFactor x inputFactor x conditionFactor x logisticsFactor

KCB-IND-010 — Production factors MUST cap output rather than stack into impossible output unless an explicit efficiency upgrade raises nominal capacity.

## Industrial classes

- local fabrication/repair;
- clean advanced manufacturing;
- research/precision;
- food/biological processing;
- construction materials;
- bulk/heavy processing;
- logistics/warehousing;
- utilities;
- cultural/digital/immaterial output.

Concordia should skew toward advanced services, research and clean high-value production rather than being dominated by bulk extraction.

## Canon material mapping

Where appropriate, use existing categories such as structural metals/alloys, conductive metals, magnetic materials, silica/ceramics, chemicals, biomass/food inputs and advanced/strategic materials.

KCB-IND-020 — Exact game recipes MUST live in the game data layer, not Universe lore, unless separately promoted.

## Inventory

Each facility has finite input storage, output storage and optional buffer capacity.

KCB-IND-030 — Full output storage throttles production.
KCB-IND-031 — Empty input storage throttles production.
KCB-IND-032 — Inventory is measured independently from goods in transit.

## Procurement

Facilities source inputs based on local supplier price, transport cost, delivery time, reliability, quality/spec if relevant and external import alternatives.

KCB-IND-040 — The cheapest nominal supplier is not necessarily chosen if transport makes it expensive.

## Warehousing

Warehouses consolidate cargo, buffer volatility and reduce repeated long deliveries, but consume land, staff and create concentrated freight.

KCB-IND-050 — Warehouse policy supports target stock level and priority categories.

## Production chains

The game SHOULD include enough chain depth to reward logistics planning without requiring hundreds of intermediate goods.

Recommended abstraction:
- raw/primary input;
- processed material;
- component/good;
- local final consumption or export.

Advanced industries may add one more specialised stage.

KCB-IND-060 — Chain depth SHOULD be determined by gameplay value, not simulation vanity.

## Specialisation clusters

Co-located related industries can gain shared suppliers, specialised labour, research spillovers and logistics efficiency.

KCB-IND-070 — Cluster benefits MUST have a physical/economic rationale and avoid a pure adjacency magic-number bonus where possible.

## Offices and immaterial production

Digital/research/service organisations can produce immaterial output.

Inputs include qualified labour, power/compute, connectivity and institutional access.

KCB-IND-080 — Immaterial sectors still create commuting, service and utility demand.

## Environmental externalities

Industrial externalities can include noise, heat, air emissions, water discharge, freight traffic and hazardous-material handling.

Advanced Commonwealth technology mitigates many impacts but does not make them zero.

KCB-IND-090 — Cleaner technology trades capital, energy, maintenance or specialised inputs for lower externalities.

## Industrial reliability

KCB-IND-100 — High-risk or complex facilities have incident/failure probability influenced by condition, staffing, maintenance and safeguards.

KCB-IND-101 — Failures are uncommon and diagnosable, not arbitrary punishment.

## Information views

Required:
- production/consumption by category;
- surplus/deficit;
- input shortage;
- inventory;
- freight cost;
- supplier/customer network;
- industrial employment;
- utility intensity;
- externalities.

KCB-IND-110 — Selecting a facility shows its current bottleneck and next-most-limiting factor.

## Acceptance criteria

- Output falls when a critical input is exhausted.
- Warehouse buffers a short disruption.
- Poor cargo access raises delivered input cost.
- Cleaner upgrade lowers environmental impact but has explicit operating/capital trade-off.
- Production statistics reconcile with inventories plus imports/exports/consumption within tolerance.
