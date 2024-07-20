#include <vector>
#include "ink_stroke_modeler/stroke_modeler.h"
#include "ink_stroke_modeler/types.h"

#ifdef _WIN32
#    define LIBRARY_API __declspec(dllexport)
#else
#    define LIBRARY_API
#endif

extern "C" {
    LIBRARY_API ink::stroke_model::StrokeModeler* nink_init_modeler(const ink::stroke_model::StrokeModelParams* params)
    {
        ink::stroke_model::StrokeModeler* modeler = new ink::stroke_model::StrokeModeler();
        if (modeler->Reset(*params).ok()) {
            return modeler;
        } else {
            delete modeler;
            return nullptr;
        }
    }

    LIBRARY_API bool nink_update(ink::stroke_model::StrokeModeler* modeler, ink::stroke_model::Input::EventType eventType, float x, float y, double time)
    {
        ink::stroke_model::Input input = { 
            .event_type{ eventType },
            .position{ ink::stroke_model::Vec2 { x: x, y: y } },
            .time{ ink::stroke_model::Time(time) },
        };
        std::vector<ink::stroke_model::Result> results;
       
        if (modeler->Update(input, results).ok()) {
            return true;
        } else {
            return false;
        }
    } 

    LIBRARY_API void nink_free_modeler(ink::stroke_model::StrokeModeler* modeler)
    {
        delete modeler;
    }
}