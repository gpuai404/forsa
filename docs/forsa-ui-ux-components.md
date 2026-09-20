# Forsa UI/UX Component System

**Product:** forsa  
**Purpose:** Reusable UI/UX specification for Forsa mobile app  
**Visual character:** messenger-inspired, product-native, calm, transparent, career-focused  
**Core interaction philosophy:** **Morph → Unfold → Expand**

---

## 1. Design Principles

### 1.1 Core product idea
> Don't make people search through jobs. Help them understand which opportunities fit them and why.

### 1.2 UX principles
- **Personal before generic:** prioritize relevance over job-board density.
- **Explain the match:** every important match score should be understandable.
- **Progressive disclosure:** show the essential answer first, details on demand.
- **Conversation over forms:** Resume AI should feel like a career conversation.
- **Movement over status:** Activity should show what is happening next, not just static states.
- **Quiet profile:** profile management should support the matching system without becoming a dashboard.

### 1.3 Interaction grammar
- **Morph:** a focused control transforms into the next state.
- **Unfold:** results/content appear progressively beneath the user's action.
- **Expand:** detail opens inline or pushes forward from the selected object.
- Avoid decorative motion that does not communicate hierarchy, cause, or feedback.

---

# 2. Foundation Tokens

## 2.1 Colors

| Token | Value | Usage |
|---|---|---|
| `forsa-blue` | `#0057B8` | Primary CTA, active nav, selected chips, match indicators, AI identity |
| `surface-page` | `#F5F7F9` | App/page background |
| `surface-card` | `#FFFFFF` | Cards, sheets, inputs |
| `text-primary` | `#111827` | Headlines, titles, primary content |
| `text-muted` | `#667085` | Supporting copy, metadata, placeholders |
| `border-subtle` | `#E4EAF1` | Dividers, input borders |
| `surface-blue-soft` | `#EAF3FF` | Selected/soft blue surfaces, match explanation blocks |
| `surface-success-soft` | `#ECFDF3` | Positive status backgrounds |
| `text-success` | `#18864B` | Success/status accent |
| `surface-warning-soft` | `#FFF7E8` | Attention states |
| `text-warning` | `#9A6700` | Warning/attention text |
| `surface-danger-soft` | `#FFF0F1` | Error/destructive surfaces |
| `text-danger` | `#C5283D` | Errors/destructive actions |

### Color rules
- Blue is a **signal**, not a page-fill color.
- Keep large areas neutral.
- Do not use blue on every icon.
- Use soft blue surfaces to create hierarchy without visual noise.

---

# 3. Typography

## 3.1 Typeface
**Primary:** Inter, or an equivalent modern sans-serif with strong mobile rendering.

## 3.2 Type scale

| Style | Weight | Size | Line height | Primary use |
|---|---:|---:|---:|---|
| Display | 900 | 32 px | 40 px | Hero/primary page headline |
| H1 | 900 | 28 px | 36 px | Major screen title |
| H2 | 800 | 24 px | 32 px | Section headline |
| Job title | 800 | 18 px | 24 px | Opportunity title |
| Section title | 700 | 16 px | 24 px | Section labels |
| Control | 600–700 | 14 px | 20 px | Buttons, chips, tabs |
| Body | 400 | 15–16 px | 22–24 px | Descriptions |
| Small body | 400 | 14 px | 20 px | Supporting text |
| Metadata | 500 | 12–13 px | 18 px | Location, salary, timestamps |
| Caption | 400–500 | 11 px | 16 px | Tiny context labels |

### Typography rules
- Use heavy weights for hierarchy, not long paragraphs.
- Keep body copy readable on 360–430 px mobile widths.
- Use sentence case for UI.
- Reserve all-caps for tiny utility labels only.
- Keep numeric match scores visually prominent.

---

# 4. Spacing

Use a **4 px base grid**.

| Token | Value |
|---|---:|
| `space-1` | 4 px |
| `space-2` | 8 px |
| `space-3` | 12 px |
| `space-4` | 16 px |
| `space-5` | 20 px |
| `space-6` | 24 px |
| `space-7` | 28 px |
| `space-8` | 32 px |
| `space-10` | 40 px |
| `space-12` | 48 px |

### Mobile layout guidance
- Screen horizontal padding: **20 px**
- Dense control gap: **8 px**
- Standard control gap: **12 px**
- Card internal padding: **16–20 px**
- Section gap: **24–32 px**

---

# 5. Shape System

## 5.1 Radius tokens

| Token | Value | Usage |
|---|---:|---|
| `radius-xs` | 8 px | Small badges, metadata containers |
| `radius-sm` | 12 px | Inputs, small cards |
| `radius-md` | 14 px | Standard controls |
| `radius-lg` | 18 px | Job cards / primary cards |
| `radius-xl` | 22 px | Large feature cards / sheets |
| `radius-pill` | 999 px | Chips, pills, match badges |
| `radius-circle` | 50% | Avatars, icon buttons |

### Shape language
- Cards: **14–22 px**
- Inputs: **12–16 px**
- Pills: full capsule
- Avatars: circular
- Avoid sharp rectangular UI unless required for content.

---

# 6. Elevation and Borders

Use restrained elevation.

### Default card
- Background: white
- Border: `1 px` `border-subtle`
- Shadow: low-opacity, soft, short blur

### Elevated surface
Use for:
- Match Lens expansion
- bottom sheets
- modal-like AI context
- floating composer

### Rule
Prefer **border + subtle shadow** over dramatic shadows.

---

# 7. Icon System

## 7.1 Icon character
Use a simple outlined icon family with:
- rounded terminals
- consistent stroke weight
- high recognition at small sizes

## 7.2 Sizes

| Size | Usage |
|---|---|
| 16 px | metadata / inline |
| 20 px | controls |
| 24 px | primary navigation |
| 28 px | featured action / empty state |

## 7.3 Core Forsa icon set

**Navigation**
- Discover
- Activity
- Resume AI
- Profile

**Search**
- Search
- Filter
- Back
- Close
- Recent/history

**Opportunity**
- Location
- Salary
- Experience
- Skills
- Work style
- Save/bookmark
- Share

**Activity**
- Application received
- Profile viewed
- Next step
- Saved job

**AI**
- Spark/AI
- Attachment
- Send
- Suggested action

---

# 8. Controls

## 8.1 Primary button

**Purpose:** dominant action.

Specifications:
- Height: **48–52 px**
- Radius: **14 px**
- Background: `forsa-blue`
- Text: white
- Weight: 700
- Horizontal padding: 18–20 px

Examples:
- `Apply for this job`
- `Save`
- `Build resume`

### States
- Default
- Pressed
- Disabled
- Loading
- Success

---

## 8.2 Secondary button

- Transparent/white surface
- 1 px blue or subtle border
- Blue text
- Same height as primary
- 12–14 px radius

Examples:
- `Back`
- `View details`
- `Edit`

---

## 8.3 Text button
Used for low-priority navigation.

Examples:
- `See all`
- `Clear`
- `View more`

Avoid making multiple text buttons compete in one row.

---

## 8.4 Search field

### Default
- Height: 48–52 px
- Radius: 14–16 px
- Soft neutral surface or white surface
- Search icon on left
- Placeholder text
- Optional close/filter action on right

### Focused
- Expand to dominant screen element in Search mode.
- Keep the transition connected to the originating Discover search.

### Error
- Preserve query content.
- Use inline error beneath the field, not a blocking modal.

---

## 8.5 Filter chips

### Structure
- Horizontal scroll
- Full-pill radius
- 12–14 px type
- 8–14 px horizontal padding

Core filters:
- For you
- Remote
- Near me
- New today
- €50k+

### States
- Default
- Selected
- Pressed
- Disabled

Selected state:
- Blue fill or very soft blue surface
- Stronger text contrast
- Optional small close icon when removable

---

## 8.6 Tags / metadata pills

Examples:
- Remote
- Full-time
- €60k–€90k
- 3+ years

Use small rounded rectangles or soft pills.
Keep them compact and scannable.

---

## 8.7 Toggle

- Compact mobile switch
- Blue = on
- Neutral = off
- Thumb always clearly separated from track

Use only for binary settings such as job alerts.

---

## 8.8 Checkbox / radio

### Checkbox
- 20 px target
- Filled blue selected state
- Rounded corners

### Radio
- Circular
- Blue selected dot
- Use when only one choice is allowed

---

## 8.9 Icon button

- 40–44 px touch target
- Circular or rounded-square hit area
- Icon centered
- Quiet default surface

Use for:
- Save
- Share
- Close
- More
- Back

---

## 8.10 Composer

For Resume AI:
- Minimum 48 px
- Rounded input container
- Attachment icon
- Placeholder
- Send button as circular action

The composer should feel like messaging, not a textarea in a form.

---

# 9. Opportunity Card

## Anatomy

1. Company/logo
2. Job title
3. Company name
4. Location / work style / employment
5. Match score
6. Match bar
7. Three reasons
8. Optional save action
9. Optional expanded Match Lens

### Default
The card should answer:
**What is this?**
**How well does it fit?**
**Why?**

### Match score
Example:
`92% Match`

Use a pill or compact score badge.

### Match bar
- Horizontal progress indicator
- Blue fill
- Neutral track
- Value shown as percentage

---

# 10. Match Lens

## Purpose
Turn matching into a transparent explanation layer.

### Trigger
Tap the match score or `Why this match?`.

### Expanded content
- Skills — 96% overlap
- Work style — 91% overlap
- Location — 100% overlap
- Experience — 84% overlap

### Interaction
- Expand inline
- Preserve scroll position
- Animate height and internal rows together
- Add a small chevron/close affordance

### Principle
The match score is useful only when the user can understand its inputs.

---

# 11. Activity Timeline

## Timeline item anatomy
- Status icon
- Event title
- Job/company context
- Relative timestamp
- Connector line to next event

Core events:
- Application received
- Profile viewed
- Next step

### Status treatment
- Active/current event: highest visual emphasis
- Completed event: clear but quieter
- Future event: low-emphasis / upcoming

---

# 12. Saved Job Row

Compact list row:
- Company logo
- Job title
- Company
- Location/salary metadata
- Save/bookmark icon

Use a row, not a large card.
Saved jobs are secondary to the application activity timeline.

---

# 13. Resume AI Components

## Chat bubble
- User bubbles aligned right
- AI bubbles aligned left
- Rounded message containers
- Timestamps subtle

## AI response card
Can contain:
- Professional experience
- Key skills
- Summary
- Suggestions

## Suggested prompts
Pills above the composer:
- Build from LinkedIn
- Improve my summary
- Tailor to this job

### Job context attachment
When launched from Job View:
- Show attached job context
- Make the selected job obvious
- Keep conversation state intact

---

# 14. Profile Components

## Profile header
- Avatar
- Name
- Role

## Profile strength card
Example:
`Profile strength — 78%`

Supporting message:
`Your profile powers the match score and Resume AI.`

## Editable rows
- Experience
- Skills & preferences
- CV / Resume
- Job alerts

Rows should be simple, tappable, and quiet.

---

# 15. Navigation

## Bottom navigation
Four destinations:

| Item | Purpose |
|---|---|
| Discover | Find opportunities |
| Activity | Track applications |
| Resume AI | Create/improve career materials |
| Profile | Manage profile |

### Active state
- Blue icon
- Blue label
- Higher contrast than inactive items

### Navigation principle
**Discover = find**  
**Activity = track**  
**Resume AI = create**  
**Profile = manage**

Job View is intentionally **not** a bottom-nav destination.

---

# 16. Illustration Style

## Character
- Minimal editorial figures
- Soft rounded forms
- Simple facial detail
- Friendly, competent, not childish

## Supporting graphics
- Cards
- document snippets
- search symbols
- AI spark
- target
- location pin
- growth bars

## Illustration rules
- Prefer blue + neutral palette.
- Keep backgrounds light.
- Use illustration to clarify a product concept, not to decorate every screen.
- Avoid large illustrations above the primary mobile action.

---

# 17. Illustration Recipes

## Career progress
Person + laptop + floating opportunity/document cards.

## Resume AI
Central AI/spark symbol + structured resume blocks.

## Goals & direction
Target/arrow motif + subtle supporting particles.

## Success
Resume/document + check.

## Discovery
Search icon + subtle opportunity cards.

## Location
Map pin or location marker, kept simple.

## Growth
Simple bars or upward movement.

## Trust
Shield/check.

---

# 18. Motion System

## 18.1 Motion principles
Motion should:
- preserve context
- indicate hierarchy
- show cause and effect
- reduce perceived waiting
- never block the user unnecessarily

## 18.2 Timing

| Motion | Duration |
|---|---:|
| Micro feedback | 120–160 ms |
| Control transition | 160–220 ms |
| Card expansion | 220–320 ms |
| Screen push | 280–360 ms |
| Search morph | 260–360 ms |
| Staggered result reveal | 80–120 ms between items |

Use a smooth ease-out for entering content and a slightly quicker ease-in for exits.

---

# 19. Signature Motions

## A. Search → Unfold
1. Discover search field expands.
2. Search state becomes visually dominant.
3. Recent searches appear.
4. “Matches unfolding” appears.
5. Result cards rise one after another.
6. Result count updates live.

## B. Match Lens → Expand
1. User taps `92% Match`.
2. Score remains anchored.
3. Lens panel expands below.
4. Rows reveal with a small stagger.
5. Card height animates smoothly.

## C. Job View push
Selected opportunity card moves/pushes forward into Job View.
Use the selected card as the visual origin.

## D. Resume AI continue
Job context flows into the conversation.
The transition should feel like the job is attached to the chat, not like a new utility is launched.

---

# 20. Component States

Every interactive component should define:

- Default
- Hover (where applicable on larger screens)
- Pressed
- Focused
- Selected
- Disabled
- Loading
- Error
- Success
- Expanded (where applicable)

### Accessibility
- Touch target: **44 px minimum**
- Never rely on color alone for state
- Maintain readable contrast
- Support dynamic type where possible
- Motion-sensitive users should be able to reduce nonessential animation

---

# 21. Responsive Behavior

## Mobile
Primary product surface. Optimize for 360–430 px widths.

## Tablet
- Increase content max-width
- Keep cards readable
- Avoid simply scaling everything up

## Desktop
Use wider content columns only where it improves information density.
Do not turn Discover into a dense multi-column job board.

---

# 22. Do / Don't

### Do
- Use blue sparingly and meaningfully.
- Make match reasoning visible.
- Keep opportunity cards concise.
- Animate the transformation of existing objects.
- Use messenger-like conversational patterns in Resume AI.

### Don't
- Use giant maps as the homepage hero.
- Use speculative “career radar” visuals as the core experience.
- Fill the interface with blue.
- Add a redundant Jobs tab.
- Hide the explanation behind a separate analytics screen.
- Make Resume AI look like a traditional form builder.
